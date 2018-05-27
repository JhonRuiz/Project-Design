//Express web server
const express = require('express');
//URL router for API calls
const router = express.Router();
//User model for user functions
const User = require('../models/user');
//AssetBundle model for AssetBundle functions
const AssetBundle = require('../models/assetbundle');
//PassportJS Authentication
const passport = require('passport');
//JWT for providing authentication tokens
const jwt = require('jsonwebtoken');
//Database configuration
const config = require('../config/database');
//Multer for AssetBundle uploads
const multer  = require('multer');
//Storage configuration for Multer
const storage = multer.diskStorage({
    //Folder where the files will be stored
    destination: function(req, file, cb){
        cb(null, uploadFolder);
    },
    //Filenames for uploaded files
    filename: function(req, file, cb) {
        cb(null, file.originalname);
    }
})
//Upload configuration of Multer
const upload = multer({ storage: storage }).single('assetbundle');
//Filesystem access
const fs = require('fs');
//Upload folder directory
const uploadFolder = './uploads/'

// Register API
// This API allows an administrator to create a new user.
router.post('/register', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Check to see the requester is an administrator and their account is active.
    if (req.user.isActive && req.user.isAdmin) {
        //Create a new User object to pass to the User model
        let newUser = new User({
            name: req.body.name,
            email: req.body.email,
            username: req.body.username,
            password: req.body.password
        })

        //Add user using the User model
        User.addUser(newUser, function(err, user) {
            //Check for errors
            if(err) {
                //Failed to add new user
                res.json({success: false, msg:'Failed to register user.'})
            }
            else {
                //User was added successfully
                res.json({success: true, msg:'User successfully registered.'})
            }
        })
    }
    //If the user is not active or not an administrator, return false with message
    else {
        //Return false with message
        return res.json({success: false, msg:'Unauthorised.'})
    }
});

// Authentication API
//Allows users to authenicate and be issued with a token.
router.post('/authenticate', function(req, res, next) {
    //Set username and password vars
    const username = req.body.username;
    const password = req.body.password;
    User.find({}, function(err, users) {
        console.log(users);
    })
    //Pass username to User model to verify user exists
    User.getUserByUsername(username, function(err, user){
        //Check for error
        if (err) throw err;
        //If user not found
        if(!user) {
            //Return false with message
            return res.json({success: false, msg: "User not found."})
        }

        //If user found, compare the given password with the password in the DB using the User model function
        User.comparePassword(password, user.password, function(err, isMatch){
            //Check for error
            if(err) throw err;
            //Require the user to be activated, or else they can't log in
            if (user.isActive) {
                //If the user passwords match, issue a token
                if(isMatch) {
                    //Issue a token to the user
                    const token = jwt.sign({data: user}, config.secret, {
                        expiresIn: 604800 // 1 week
                 });
                    //Pass the token and user info back to the user as JSON object
                    res.json({
                        success: true,
                        token: 'JWT ' +token,
                        user: {
                            id: user._id,
                            name: user.name,
                            username: user.username,
                            email: user.email
                        }
                    })
                }
                //If the passwords don't match, return false with message
                else {
                    //Return false with message
                    return res.json({success: false, msg: "Wrong password."})
                }
            }
            //If the user isn't activated, return false with message
            else {
                return res.json({success: false, msg: "Unauthorized."})
            }
        });
    })
});

// Profile API
// Allows a user's own details to be sent back to the user
router.get('/profile', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Require the user account to be active
    if (req.user.isActive) {
        //Send specific user detail back (ID, Name, Username, Email)
        res.json({user: {
                            id: req.user._id,
                            name: req.user.name,
                            username: req.user.username,
                            email: req.user.email
                        }
                    })
        
    }
    //If user account isn't active, return false with message
    else {
        //Return false with message
        return res.json({success: false, msg: "Unauthorized"});
    }
});

// Retrieving All Users API
// Allows administrators to get user details that exist in the database
router.get('/getAllUsers', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Check the user is active and is an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Call the getAllUsers function from the Users model
        User.getAllUsers(function(err, users) {
            //User details will be selectivly put into a new array - not all details (e.g. passwords) need to be delivered
            var array = [];
            //For each user returned, add to array
            users.forEach(function(element) {
                //Add to array
                array.push({
                    id: element._id,
                    name: element.name,
                    username: element.username,
                    email: element.email,
                    isActive: element.isActive,
                    isAdmin: element.isAdmin
                })
            })
            //Return the array
            res.json(array);
        })
    } 
    //If user account is not active or not an administrator, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Deleting Users API
router.post('/deleteUser', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //Check to see if user is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Call the deleteUser function on the user model
        User.deleteUser(req.body.id, function(err) {
            //Check for errors, if no errors
            if (!err) {
                //Return successful with message
                res.json({success: true, msg: "User successfully deleted."})
            }
            //If there were errors
            else {
                //Return false with message
                res.json({success: false, msg: "User unable to be deleted."})
            }
        })
    }
    //If the user account is not active or an administrator, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Remove AssetBundle API
// API for removing uploaded AssetBundles. Deletes the file and DB entry
router.post('/removeBundle', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //Check to see that the user is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Find the assetbundle using the AssetBundle model function by ID
        AssetBundle.findById(req.body.id, function(err, result) {
            //Store the resut in variable so that it can be accessed after DB entry deletion
            let path = result;
            //If no errors
            if (!err) {
                //Call the removeBundle function from the AssetBundle model
                AssetBundle.removeBundle(req.body.id, function(err) {
                    //If no errors removing bundle
                    if (!err) {
                        //Delete the file from the uploads directory
                        fs.unlink(uploadFolder + path.fileLocation, function(err) {
                            //Check for an error
                            if (err) {
                               //If error, return false with message
                               return res.json({success: false, msg: "Could not delete file."})
                            }
                            //If no errors, return success with message
                            else {
                                //Return true with message
                                return res.json({success: true, msg: "Successfully deleted file."})
                            }
                        }) 
                    } 
                })
            } 
            //If error, return false with message
            else {
                //Return false with message
                return res.json({success: false, msg: "Could not find bundle."})
            }
        })
    } 
    //If the user is not active or an administrator, return false with message
    else {
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Disable Account API
// Allows administrators to disable user accounts without deleting them
router.post('/disableAccount', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //If the user is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Call the disableAccount function from the user model
        User.disableAccount(req.body.id, function(err) {
            //Check for errors, if none
            if (!err) {
                //Return successful with message
                res.json({success: true, msg: "User successfully disabled."})
            }
            //Otherwise, return false with message
            else {
                res.json({success: false, msg: "User could not be disabled."})
            }
        })
    } 
    //If the user is not active or an admin, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    } 
});

//Enable Account API
//Allows administrators to re-enable accounts that have been disabled.
router.post('/enableAccount', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //Check that the user is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Activate account using the user model function enableAccount
        User.enableAccount(req.body.id, function(err) {
            //Check for errors, if none
            if (!err) {
                //Return successful with message
                res.json({success: true, msg: "User successfully enabled."})
            }
            //Otherwise if error
            else {
                //Return false with message
                res.json({success: false, msg: "User could not be enabled."})
            }
        })
    } 
    //If user is not active or an administrator, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Enable Administrator API
// Allows administrators to set other users as administrators
router.post('/enableAdmin', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //Check that the user account is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Run the enableAdmin function from the user model
        User.enableAdmin(req.body.id, function(err) {
            //Check for errors, if none
            if (!err) {
                //Return successful with message
                res.json({success: true, msg: "User account successfully set as administrator."})
            }
            //Otherwise if not successful
            else {
                //Return false with message
                res.json({success: false, msg: "User account could not be set to administrator."})
            }
        })
    } 
    //If user account is not active or an administrator, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Disable Administrator API
// Allows administrators to remove admin status from other accounts
router.post('/disableAdmin', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //Check to see if user is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Disable the sent account using the disableAdmin user model function.
        User.disableAdmin(req.body.id, function(err) {
            //Check for errors, if none
            if (!err) {
                //Return true with message
                res.json({success: true, msg: "Administrator role successfull removed from account."})
            }
            //Otherwise, if error
            else {
                //Return false with message
                res.json({success: false, msg: "Unable to remove administrator role from account."})
            }
        })
    } 
    //If account is not active or an administrator
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
});

// Upload AssetBundle API
// Allows an administrator to upload new AssetBundles
router.post('/uploadAsset', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Check to see if requesting user's account is active and an administrator
    if (req.user.isActive && req.user.isAdmin) {
        //Upload the file to the server
        upload(req, res, function(err) {
            //If error exists, return false with message
            if (err) {
                //Return false with message
                return res.json({success: false, msg: "An error occured while uploading."});
            }
            //Check to see whether file already exists inside the database
            AssetBundle.findByFileName(req.file.originalname, function(err, data) {
                //If error, return false with message
                if (err) {
                    //Return false with message
                    return res.json({success: false, msg: "An error occured"});
                }
                //If file already exists, return false with message
                else if (data) {
                    //Return false with message
                    return res.json({success: false, msg: "File already exists."})
                }
                //Otherwise, upload the file details to database
                else {
                    //Create new AssetBundle with POST'd details
                    //console.log(req.file.platform);
                    let newBundle = AssetBundle({ title: req.body.title, prefab: req.body.prefab, fileLocation: req.file.originalname, platform: req.body.platform});
                    //Add the bundle to database using addBundle function from AssetBundle model
                    AssetBundle.addBundle(newBundle, function(err) {
                        //If error, return false with message
                        if (err) {
                            //Return false with message
                            return res.json({success: false, msg: "File could not be uploaded." + err})
                        }
                        //If no error, file was successfully added to the database.
                        return res.json({success: true, msg: "File uploaded sucessfully"})
                    })
                } 
            });
        })
    }
    //If user is not active or not an admin, return false with message
    else {
        //Return false with message
        res.json({success: false, msg: "Unauthorized"});
    }
})

// Get All AssetBundles API
// Allows users to get details of all available AssetBundles
router.get('/getAllAssetBundles', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Check that the user is active
    if (req.user.isActive) {
        //Request all AssetBundles from AssetBundle model using getAllAssetBundles function
        AssetBundle.getAllAssetBundles(function (err, bundleArray) {
            //Check for errors, return false with message
            if (err) {
                return res.json({success: false, msg: "Error getting assetbundles."})
            }
            //Otherwise, return true with bundle array (jsonData)
            res.json({success: true, bundles: bundleArray});
        })
    }
})

// Download Assetbundle API
// Allows authorised users to download assetbundles from server
router.get('/AssetBundleDownload', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    //Check that the user account is active
    if (!req.user.isActive) {
        //Return false with message
        return res.json({success: false, msg: "Unauthorized."})
    }
    //Otherwise, account can download models.
    else {
        //Variable for file URL
        var file = uploadFolder + req.query.fileName;
        //Respond to GET with file download
        res.download(file, req.query.fileName, function(err) {
            //If error the file doesn't exist, respond false with message
            if (err) {
                //Return false with message
                res.json({success: false, msg: "File does not exist."});
            }
        })
     }
})

//Module Exports for router
module.exports = router;
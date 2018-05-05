const express = require('express');
const router = express.Router();
const User = require('../models/user');
const AssetBundle = require('../models/assetbundle');
const passport = require('passport');
const jwt = require('jsonwebtoken');
const config = require('../config/database');
var path = require('path')
const multer  = require('multer');
const storage = multer.diskStorage({
    destination: function(req, file, cb){
        cb(null, './uploads');
    },
    filename: function(req, file, cb) {
        cb(null, file.originalname);
    }
})
const upload = multer({ storage: storage }).single('assetbundle');
const fs = require('fs');

// Register
router.post('/register', function(req, res, next) {
    let newUser = new User({
        name: req.body.name,
        email: req.body.email,
        username: req.body.username,
        password: req.body.password
    })

    User.addUser(newUser, function(err, user) {
        if(err) {
            res.json({success: false, msg:'Failed to register user'})
        }
        else {
            res.json({success: true, msg:'User registered'})
        }
    })

});

// Authenticate
router.post('/authenticate', function(req, res, next) {
    const username = req.body.username;
    const password = req.body.password;
    console.log(req.body.username);

    User.getUserByUsername(username, function(err, user){
        if (err) throw err;
        if(!user) {
            return res.json({success: false, msg: "User not found."})
        }

        User.comparePassword(password, user.password, function(err, isMatch){
            if(err) throw err;
            if (user.isActive) {
                if(isMatch) {
                    const token = jwt.sign({data: user}, config.secret, {
                        expiresIn: 604800 // 1 week
                 });
    
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
                } else {
                    return res.json({success: false, msg: "Wrong password."})
                }
            }
            else {
                return res.json({success: false, msg: "Unauthorized."})
            }
            
        });
    })

});

// Profile
router.get('/profile', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    if (req.user.isActive) {
        res.json({user: req.user})
    }
    else {
        res.json({success: false, msg: "Unauthorized"});
    }
    

});

router.get('/getAllUsers', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        User.getAllUsers(function(err,users) {
            var array = [];
            users.forEach(function(element) {
                array.push({
                    id: element._id,
                    name: element.name,
                    username: element.username,
                    email: element.email,
                    isActive: element.isActive,
                    isAdmin: element.isAdmin
                })
            })
            res.json(array);
        })
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }

    

});

router.post('/deleteUser', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //console.log(req.user);
    if (req.user.isActive && req.user.isAdmin) {
        User.deleteUser(req.body.id, function(err) {
            console.log(err);
            if (!err) {
                res.json({success: true, msg: "deleted user"})
            }
            else {
                res.json({success: false, msg: "not successful"})
            }
        })
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    

});

router.post('/removeBundle', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    //console.log(req.user);
    if (req.user.isActive && req.user.isAdmin) {
        
        console.log("ID is " + req.body.id);
        AssetBundle.findById(req.body.id, function(err, result) {
            let path = result;
            if (!err) {
                AssetBundle.removeBundle(req.body.id, function(err) {
                    
                    if (!err) {
                        
                        fs.unlink("./uploads/" + path.fileLocation, function(err) {
                        if (err) {
                            console.log(err);
                            return res.json({success: false, msg: "Could not delete file."})
                        }
                        else {
                            return res.json({success: true, msg: "Successfully deleted file."})
                        }
                          
                    }) 
                } 
                })
            } else {
                return res.json({success: false, msg: "Could not find bundle in DB"})
            }
        })

        

        
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    

});

router.post('/disableAccount', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        //console.log(req.user);
        User.disableAccount(req.body.id, function(err) {
            console.log(err);
            if (!err) {
                res.json({success: true, msg: "disabled user"})
         }
          else {
              res.json({success: false, msg: "not successful"})
         }
        })

    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    
});

router.post('/enableAccount', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        //console.log(req.user);
    User.enableAccount(req.body.id, function(err) {
        console.log(err);
        if (!err) {
            res.json({success: true, msg: "enabled user"})
        }
        else {
            res.json({success: false, msg: "not successful"})
        }
    })
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    

});

router.post('/enableAdmin', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        User.enableAdmin(req.body.id, function(err) {
            console.log(err);
            if (!err) {
                res.json({success: true, msg: "enabled admin"})
            }
            else {
                res.json({success: false, msg: "not successful"})
            }
        })
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    //console.log(req.user);
    

});

router.post('/disableAdmin', passport.authenticate('jwt', {session:false}),function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        User.disableAdmin(req.body.id, function(err) {
            console.log(err);
            if (!err) {
                res.json({success: true, msg: "disabled admin"})
            }
            else {
                res.json({success: false, msg: "not successful"})
            }
        })
    } else {
        res.json({success: false, msg: "Unauthorized"});
    }
    //console.log(req.user);
    

});

router.post('/uploadAsset', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    if (req.user.isActive && req.user.isAdmin) {
        upload(req, res, function(err) {
            
            if (err) {
                return res.json({
                    success: false,
                    msg: "An error occured"
                });
            }

            AssetBundle.findByFileName(req.file.originalname, function(err, data) {
                if (err) {
                    return res.json({
                        success: false,
                        msg: "An error occured"
                    })
                }
                else if (data) {
                    return res.json({
                        success: false,
                        msg: "File already exists."
                    })
                }
                else {
                    let newBundle = AssetBundle({ title: req.body.title, prefab: req.body.prefab, fileLocation: req.file.originalname});
            AssetBundle.addBundle(newBundle, function(err) {
                if (err) {
                    return res.json({
                        success: false,
                        msg: "File could not be uploaded."
                    })
                }
                return res.json({
                    success: true,
                    msg: "File uploaded sucessfully"
                })
                
            })
                }
                
                
            });
            
            
            //console.log(req.body);
        })
        //console.log(req.body);
    }
    else {
        res.json({success: false, msg: "Unauthorized"});
    }
    

})

router.get('/getAllAssetBundles', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    if (req.user.isActive) {
        AssetBundle.getAllAssetBundles(function (err, jsondata) {
            
            if (err) {

            }
            res.json({
                success: true,
                bundles: jsondata
            });

        })
    }
})

router.get('/AssetBundleDownload', passport.authenticate('jwt', {session:false}), function(req, res, next) {
    if (!req.user.isActive) {
        return res.json({success: false, msg: "Account not active"})
    }
     else {
        var file = './uploads/'+ req.query.fileName;
        res.download(file);
     }
    

})

module.exports = router;
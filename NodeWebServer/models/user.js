//Mongoose for DB connection
const mongoose = require('mongoose');
//Bcrypt to encrypt user passwords
const bcrypt = require('bcryptjs');

//User Schema for DB Objects
const UserSchema = mongoose.Schema({
    name: {
        type: String
    },
    email: {
        type: String,
        required: true
    },
    username: {
        type: String,
        required: true
    },
    password: {
        type: String,
        required: true
    },
    isAdmin: {
        type: Boolean,
        required: true
    },
    isActive: {
        type: Boolean,
        required: true
    }
});

//Set User to be the database model
const User = module.exports = mongoose.model('User', UserSchema);

//Search for user by ID
module.exports.getUserById = function(id, callback) {
    //Find user by ID in database
    User.findById(id, callback);
}

//Retrieve all users in database
module.exports.getAllUsers = function(callback) {
    //Find all user database objects
    
    User.find({}, callback);
}

//Search for user by username
module.exports.getUserByUsername = function(username, callback) {

    const query = {username: username};
    //Get the first user retrieved with matching username
    User.findOne(query, callback);
}

//Add a new user to database
module.exports.addUser = function(newUser, callback) {
    //Generate salt for encrypting user password
    bcrypt.genSalt(10, function(err, salt) {
        //Hash the user's password with the salt
        bcrypt.hash(newUser.password, salt, function(err, hash) {
            //Throw if error occured
            if(err) throw err;
            //Set the password to be equal to the hash
            newUser.password = hash;
            //The user will not be an admin by default
            newUser.isAdmin = false;
            //New users will be active by default
            newUser.isActive = true;
            //Save new user to the database
            newUser.save(callback);
        })
    });
}

//Delete a user from the database
module.exports.deleteUser = function(id, callback) {
    //Find the user by ID, remove the user
    User.find({ _id: id}).remove(callback);
}

//Compare a user's password hash with the hash in DB (for login)
module.exports.comparePassword = function(candidatePassword, hash, callback) {
    //Compare the hash with the password in the database
    bcrypt.compare(candidatePassword, hash, function(err, isMatch) {
        //Throw is error
        if(err) throw err;
        //Callback function to display if passwords match or not
        callback(null, isMatch);
    })
}

//Disable a user account
module.exports.disableAccount = function(id, callback) {
    //Find the account by ID and update isActive to false
    User.find({ _id: id}).update({isActive: false}, callback);
}

//Enable a user account
module.exports.enableAccount = function(id, callback) {
    User.find({ _id: id}).update({isActive: true}, callback);
}

//Enable Administrator access for a user account
module.exports.enableAdmin = function(id, callback) {
    //Find the account by ID and update isAdmin to true
    User.find({ _id: id}).update({isAdmin: true}, callback);
}

//Disable administrator access for a user account
module.exports.disableAdmin = function(id, callback) {
    //Find the user account and set isAdmin to false
    User.find({ _id: id}).update({isAdmin: false}, callback);
}
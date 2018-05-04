const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const config = require('../config/database');

//User Schema
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

const User = module.exports = mongoose.model('User', UserSchema);

module.exports.getUserById = function(id, callback) {
    User.findById(id, callback);
}

module.exports.getAllUsers = function(callback) {
    console.log("called");
    User.find({}, callback);
}

module.exports.getUserByUsername = function(username, callback) {
    const query = {username: username};

    User.findOne(query, callback);
}

module.exports.addUser = function(newUser, callback) {
    bcrypt.genSalt(10, function(err, salt) {
        bcrypt.hash(newUser.password, salt, function(err, hash) {
            if(err) throw err;
            newUser.password = hash;
            newUser.isAdmin = false;
            newUser.isActive = true;
            newUser.save(callback);
        })
    });
}

module.exports.deleteUser = function(id, callback) {
    //console.log("ID passed " + id);
    User.find({ _id: id}).remove(callback);
}

module.exports.comparePassword = function(candidatePassword, hash, callback) {
    bcrypt.compare(candidatePassword, hash, function(err, isMatch) {
        if(err) throw err;
        callback(null, isMatch);
    })
}

module.exports.disableAccount = function(id, callback) {
    User.find({ _id: id}).update({isActive: false}, callback);
}

module.exports.enableAccount = function(id, callback) {
    User.find({ _id: id}).update({isActive: true}, callback);
}

module.exports.enableAdmin = function(id, callback) {
    User.find({ _id: id}).update({isAdmin: true}, callback);
    
}

module.exports.disableAdmin = function(id, callback) {
    User.find({ _id: id}).update({isAdmin: false}, callback);
}
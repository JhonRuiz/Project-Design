const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const config = require('../config/database');

//User Schema
const AssetBundleSchema = mongoose.Schema({
    title: {
        type: String
    },
    prefab: {
        type: String,
        required: true
    },
    fileLocation: {
        type: String,
        required: true
    }
});

const AssetBundle = module.exports = mongoose.model('AssetBundle', AssetBundleSchema);

module.exports.addBundle = function(Bundle, callback) {
    
   Bundle.save(callback);
}

module.exports.getAllAssetBundles = function(callback) {
    //console.log("called");
    AssetBundle.find({}, callback);
}

module.exports.findByFileName = function(fileName, callback) {
    const query = {fileLocation: fileName};

    AssetBundle.findOne(query, callback);
}

module.exports.findByID = function(id, callback) {
    const query = {_id: id};

    AssetBundle.findOne(query, callback);
}


module.exports.removeBundle = function(id, callback) {
    AssetBundle.find({ _id: id}).remove(callback);
}

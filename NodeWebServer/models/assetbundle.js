//Mongoose to connect with Mongo/Cosmo DB
const mongoose = require('mongoose');

//AssetBundle Schema
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
    },
    platform: {
        type: String,
        required: true
    }
});

//Creating the AssetBundle object to interact with the Database
const AssetBundle = module.exports = mongoose.model('AssetBundle', AssetBundleSchema);

//Save a bundle to the database
module.exports.addBundle = function(Bundle, callback) {
    //Save passed through object
    Bundle.save(callback);
}

//Get all AssetBundles from database
module.exports.getAllAssetBundles = function(callback) {
    //Find all database objects
    AssetBundle.find({}, callback);
}

//Find an assetbundle by file name
module.exports.findByFileName = function(fileName, callback) {
    const query = {fileLocation: fileName};
    //Find and return the first bundle matching the file name
    AssetBundle.findOne(query, callback);
}

//Find asset bundle by ID
module.exports.findByID = function(id, callback) {
    const query = {_id: id};
    //Find and return the first asset bundle by id
    AssetBundle.findOne(query, callback);
}

//Remove an AssetBundle from the database
module.exports.removeBundle = function(id, callback) {
    //Find asset bundle by ID and remove it from the database
    AssetBundle.find({ _id: id}).remove(callback);
}

//Express web server
const express = require('express');
//Use app as Express server object
const app = express();
//Path
const path = require('path');
//BodyParser for REST API
const bodyParser = require('body-parser');
//Cross Origin Resource Sharing
const cors = require('cors');
//Passport for account authentication
const passport = require('passport');
//Mongoose to connect to Mongo/Cosmo DB
const mongoose = require('mongoose');
//Database config file
const config = require('./config/database');
// Routes to use for API
const API = require('./routes/API');
// Port Number for server
const port = process.env.PORT || 8080;



// Connect to Database
mongoose.connect('mongodb://' + config.host + ':' + config.port + '/'+ config.database, {
    auth: {
      user: config.username,
      password: config.password,
      authdb: "admin"
    }
});

// On Connection
mongoose.connection.on('connected', function(){
    //Display message
    console.log("Connected to database " + config.database);
})

// On Error
mongoose.connection.on('error', function(err){
    //Display error
    console.log("Database error " + err);
})

// Body Parser Middleware
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// Passport Middleware
app.use(passport.initialize());
app.use(passport.session());
require('./config/passport')(passport);


//CORS Middleware
app.use(cors());


// Set public folder for WWW access
app.use('/', express.static(__dirname + '/public'));

//Set API routes
app.use('/API', API);

// Index Route
app.get('/', function(req,res) {
    res.send('Invalid Endpoint');
});

//Wildcard route - send to Index
app.get('*', (req,res) => {
    res.sendFile(path.join(__dirname, "public/index.html"));
})


// Start Server
app.listen(port,function() {
    console.log('Server started on port ' + port);
})

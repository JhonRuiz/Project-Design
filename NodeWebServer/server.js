const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const cors = require('cors');
const passport = require('passport');
const mongoose = require('mongoose');
const config = require('./config/database');
const qs = require('qs');

// Connect to Database
mongoose.connect('mongodb://test-id-azure.documents.azure.com:10255/arcar?ssl=true', {
    auth: {
      user: 'test-id-azure',
      password: 'jBjv01h0WR3OCJsRorcW7kO4HeewDeDLIndm1DvPm9hjU7sNH4P73cUiTLl85Z108e1QKHuSenTdL4oQXjnqyA=='
    }
  });

// On Connection
mongoose.connection.on('connected', function(){
    console.log("Connected to database " + config.database);
})

// On Error
mongoose.connection.on('error', function(err){
    console.log("Database error " + err);
})

const app = express();

const users = require('./routes/users');

// Port Number
const port = process.env.PORT || 8080;


// Body Parser Middleware
app.use(bodyParser.urlencoded({ extended: true }));

app.use(bodyParser.json());

// Passport Middleware
app.use(passport.initialize());
app.use(passport.session());

require('./config/passport')(passport);


//CORS Middleware
app.use(cors());

// Set Static Folder
app.use('/', express.static(__dirname + '/public'));



app.use('/users', users);

// Index Route
app.get('/', function(req,res) {
    res.send('Invalid Endpoint');

});

app.get('*', (req,res) => {
    res.sendFile(path.join(__dirname, "public/index.html"));
})

// Start Server
app.listen(port,function() {
    console.log('Server started on port ' + port);
})

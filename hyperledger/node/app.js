var bodyParser = require('body-parser');

var createError = require('http-errors');
var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var initProfile = require('./routes/initProfile');
var issueProof = require('./routes/issueProof');
var getProfile = require('./routes/getProfile');
var getProfileByOrgAddress = require('./routes/getProfileByOrgAddress');
var getOrg = require('./routes/getOrg');
var initOrg = require('./routes/initOrg');
var signProfile = require('./routes/signProfile');

var getProfileByPortfolioAddress = require('./routes/getProfileByPortfolioAddress');
var sendProfile = require('./routes/sendProfile');


var app = express();

app.use(function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/initProfile', initProfile);
app.use('/issueProof', issueProof);
app.use('/getProfile', getProfile);
app.use('/getProfileByOrgAddress', getProfileByOrgAddress);

app.use('/getOrg', getOrg);
app.use('/initOrg', initOrg);
app.use('/signProfile', signProfile);

app.use('/sendProfile', sendProfile);
app.use('/getProfileByPortfolioAddress', getProfileByPortfolioAddress);


app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(bodyParser.json());

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  next(createError(404));
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;

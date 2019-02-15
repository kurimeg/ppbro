var f = require('./chainCode.js');

var express = require('express');
var router = express.Router();

router.post('/', function(req, res, next) {
  var str = f.invoke('fabcar','initProfile',[req.body.address,req.body.orgAddress,req.body.mySign,req.body.orgSign,req.body.pubKey],res);
});

module.exports = router;


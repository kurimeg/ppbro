var f = require('./chainCode.js');

var express = require('express');
var router = express.Router();

router.post('/', function(req, res, next) {
  var param = [];
  param[0] = req.body.address;
  param[1] = req.body.limitDate;
  param[2] = req.body.mySign;
  param[3] = req.body.pubKey;
  
  for (var i=0; i<req.body.profileAddressList.length; i++) {
    param[i+4] = req.body.profileAddressList[i];
  }

  var str = f.invoke('fabcar','sendProfile',param,res);
});

module.exports = router;


var f = require('./chainCode.js');

var express = require('express');
var router = express.Router();

router.post('/', function(req, res, next) {
  var str = f.invoke('fabcar','issueProof',[req.body.address,req.body.id,req.body.value,req.body.orgSign],res);
});

module.exports = router;


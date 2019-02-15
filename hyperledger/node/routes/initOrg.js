var f = require('./chainCode.js');

var express = require('express');
var router = express.Router();

router.post('/', function(req, res, next) {
  var str = f.invoke('fabcar','initOrg',[],res);
});

module.exports = router;


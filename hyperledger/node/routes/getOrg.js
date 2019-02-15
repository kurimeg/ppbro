var f = require('./chainCode.js');

var express = require('express');
var router = express.Router();

var p = function(response){	
	return response;
}

router.post('/', function(req, res,next) {
  var str = f.get('fabcar','getOrg',[],p,res);
});

module.exports = router;



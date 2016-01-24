# JsExpanse
JavaScript Web Server and MVC Framework based on JINT - https://github.com/sebastienros/jint

* Create main.js
```javascript
var myCarModule = require('myCarModule.js');

var car = myCarModule.exports.getCar();

info(car.type);

info(toJson(car));
```

* Create myCarModule.js
```javascript
module.exports = {
  getCar : function() {
    return {type:"Fiat", model:"500", color:"white"};
  }
}
```
* Run *expanse -p main.js* and you will get something like this
```
JsExpanse 0.0.2.9 (Javascript interpreter based on Jint 2.6.0.0)
Fiat
{"type":"Fiat","model":"500","color":"white"}
```
TODO mvc description

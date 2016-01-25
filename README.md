# Expanse.js
JavaScript Web Server and MVC Framework based on JINT - https://github.com/sebastienros/jint

## How to

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
* Run __expanse -r main.js__ and you will get something like this
```
JsExpanse 0.0.2.9 (Javascript interpreter based on Jint 2.6.0.0)
Fiat
{"type":"Fiat","model":"500","color":"white"}
```
## How to render Razor HTML templates
* Change your main.js to
```javascript
var myCarModule = require('myCarModule.js');

var car = myCarModule.exports.getCar();

info(runRazor('index.cshtml', car));
```
* Create index.cshtml
```html
<body>
<head>Template testing</head>
<p>@Model.Exports.type</p>
</body>
```
Note, your model will be in __Exports__ property of __@Model__
* Run __expanse -r main.js__ and you will get something like this
```
JsExpanse 0.0.2.9 (Javascript interpreter based on Jint 2.6.0.0)
<body>
<head>Template testing</head>
<p>Fiat</p>
</body>
```
## How to create a project
*TODO*
## How to create a MVC project
*TODO*
## Dependencies
- Ninject https://github.com/ninject/Ninject
- Ninject.Extensions.Factory https://github.com/ninject/Ninject.Extensions.Factory
- NLog https://github.com/NLog/NLog
- RazorEngine https://github.com/Antaris/RazorEngine
- Newtonsoft.Json https://github.com/JamesNK/Newtonsoft.Json
- Jint https://github.com/sebastienros/jint
- Fluent Command Line Parser https://github.com/fclp/fluent-command-line-parser

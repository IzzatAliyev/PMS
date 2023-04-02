var colorPreference = getCookie("primaryColor");

console.log(colorPreference);

if (colorPreference === null) {
  colorPreference = "#fff";
}

document.documentElement.style.setProperty("--primary-color", colorPreference);

function getCookie(name) {
  var value = "; " + document.cookie;
  var parts = value.split("; " + name + "=");
  if (parts.length == 2) return parts.pop().split(";").shift();
}

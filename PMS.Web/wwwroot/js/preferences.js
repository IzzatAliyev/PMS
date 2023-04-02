var colorPreference = getCookie("colorPreference");

console.log(colorPreference);

if (colorPreference == "light") {
  document.documentElement.style.setProperty("--primary-color", "white");
  document.documentElement.style.setProperty("--secondary-color", "rgba(238.00000101327896, 242.00000077486038, 245.00000059604645, 1)");
  document.documentElement.style.setProperty("--third-color", "rgb(255, 248, 248)");
  document.documentElement.style.setProperty("--text-color", "black");
  document.documentElement.style.setProperty("--default-color", "rgba(111.5625, 111.5625, 111.5625, 1)");
  document.documentElement.style.setProperty("--button-color", "#333");
}
else if (colorPreference == "dark") {
  document.documentElement.style.setProperty("--primary-color", "#202123");
  document.documentElement.style.setProperty("--secondary-color", "#353740");
  document.documentElement.style.setProperty("--third-color", "#444654");
  document.documentElement.style.setProperty("--text-color", "white");
  document.documentElement.style.setProperty("--default-color", "#ece");
  document.documentElement.style.setProperty("--button-color", "#eee");
}
else {
  document.documentElement.style.setProperty("--primary-color", "white");
  document.documentElement.style.setProperty("--secondary-color", "rgba(238.00000101327896, 242.00000077486038, 245.00000059604645, 1)");
  document.documentElement.style.setProperty("--third-color", "rgb(255, 248, 248)");
  document.documentElement.style.setProperty("--text-color", "black");
  document.documentElement.style.setProperty("--default-color", "rgba(111.5625, 111.5625, 111.5625, 1)");
  document.documentElement.style.setProperty("--button-color", "#333");
}

function getCookie(name) {
  var value = "; " + document.cookie;
  var parts = value.split("; " + name + "=");
  if (parts.length == 2) return parts.pop().split(";").shift();
}

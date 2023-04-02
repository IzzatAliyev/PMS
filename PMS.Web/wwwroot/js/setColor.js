const form = document.getElementById('formcolor');
const button = document.getElementById('buttoncolor');

button.addEventListener('click', (event) => {
    event.preventDefault();
    const colorPreference = form.querySelector('input[name="colorPreference"]:checked').value;
    setCookie('colorPreference', colorPreference);
    location.reload();
});

const lightRadio = document.getElementById('light');
const darkRadio = document.getElementById('dark');
if (getCookie('colorPreference') == "dark") {
    darkRadio.checked = true;
    lightRadio.checked = false;
} else if (getCookie('colorPreference') == "light") {
    darkRadio.checked = false;
    lightRadio.checked = true;
}

function getCookie(name) {
    const cookieString = document.cookie;
    const cookies = cookieString.split('; ');
    for (const cookie of cookies) {
        const [cookieName, cookieValue] = cookie.split('=');
        if (cookieName === name) {
            return cookieValue;
        }
    }
    return null;
}

function setCookie(name, value, expires = '') {
    const cookieString = `${name}=${value}; ${expires}`;
    document.cookie = cookieString;
}
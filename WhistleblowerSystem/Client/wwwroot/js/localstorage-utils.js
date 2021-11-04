window.app = {};

window.app.setToLocalStorage = (key, value) => {
    localStorage.setItem(key, value);
}

window.app.getFromLocalStorage = (key) => {
    return localStorage.getItem(key);
}
window.sessionStorageInterop = {
    setItem: function(key, value) {
        sessionStorage.setItem(key, value);
    },
    getItem: function(key) {
        return sessionStorage.getItem(key);
    }
};
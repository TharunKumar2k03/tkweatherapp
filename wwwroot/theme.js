window.setTheme = (theme) => {
    document.body.classList.remove("light", "dark");
    document.body.classList.add(theme);
};

window.stopEventPropagation = (element) => {
    element.addEventListener("click", (event) => {
        event.stopPropagation();
    });
};

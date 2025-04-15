window.openSidebar = function() {
    // Add your sidebar opening logic here
    // This depends on how your sidebar is implemented
};

window.getFavoritesPosition = function() {
    const favoritesLink = document.querySelector('[href="/favorites"]');
    return favoritesLink ? favoritesLink.getBoundingClientRect() : null;
};

window.animateHeart = function(targetRect) {
    const heart = document.createElement('div');
    heart.innerHTML = '❤️';
    heart.style.position = 'fixed';
    heart.style.zIndex = '9999';
    heart.style.fontSize = '24px';
    heart.classList.add('heart-animation');
    
    // Set initial position (from like button)
    heart.style.top = '15px';
    heart.style.right = '15px';
    
    // Calculate final position
    const finalX = targetRect.left - heart.offsetLeft;
    const finalY = targetRect.top - heart.offsetTop;
    
    heart.style.setProperty('--final-x', `${finalX}px`);
    heart.style.setProperty('--final-y', `${finalY}px`);
    
    document.body.appendChild(heart);
    
    // Remove the element after animation
    heart.addEventListener('animationend', () => {
        document.body.removeChild(heart);
    });
}; 
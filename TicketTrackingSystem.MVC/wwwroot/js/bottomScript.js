(() => {
    'use strict';

    const getStoredTheme = () => localStorage.getItem('theme');
    const setStoredTheme = theme => localStorage.setItem('theme', theme);

    const getPreferredTheme = () => {
        const storedTheme = getStoredTheme();
        if (storedTheme) {
            return storedTheme;
        }

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    };

    const updateThemeButton = (theme) => {
        const toggleThemeBtn = document.getElementById('toggleThemeBtn');
        const isDarkMode = theme === 'dark' || (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches);

        if (isDarkMode) {
            toggleThemeBtn.innerHTML = '<i class="bi bi-moon"></i> Dark Mode';
        } else {
            toggleThemeBtn.innerHTML = '<i class="bi bi-sun"></i> Light Mode';
        }
    };

    const setTheme = theme => {
        if (theme === 'auto') {
            document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
        } else {
            document.documentElement.setAttribute('data-bs-theme', theme);
        }
        updateThemeButton(theme);  // Ensure button reflects the active theme
    };

    // Set the theme on page load
    setTheme(getPreferredTheme());

    // Handle theme switcher on page load
    window.addEventListener('DOMContentLoaded', () => {
        const toggleThemeBtn = document.getElementById('toggleThemeBtn');

        // Update the button text based on the current theme
        updateThemeButton(getPreferredTheme());

        // Handle theme toggle button click
        toggleThemeBtn.addEventListener('click', () => {
            const currentTheme = getStoredTheme();
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';  // Toggle between 'dark' and 'light'
            setStoredTheme(newTheme);
            setTheme(newTheme);  // Update the theme and button
        });
    });

    // Auto update theme if system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        const storedTheme = getStoredTheme();
        if (storedTheme !== 'light' && storedTheme !== 'dark') {
            setTheme(getPreferredTheme());  // Only auto update theme if not manually set
        }
    });
})();

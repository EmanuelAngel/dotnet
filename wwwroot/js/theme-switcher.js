// Capture toggle
const themeToggle = document.querySelector('.ThemeSwitcher-checkbox')

// Verify if a preference is set
const savedTheme = localStorage.getItem('theme')
if (savedTheme) {
  document.documentElement.setAttribute('data-theme', savedTheme)
  themeToggle.checked = savedTheme === 'dark'
}

// Change handler
themeToggle.addEventListener('change', () => {
  const newTheme = themeToggle.checked ? 'dark' : 'light'
  document.documentElement.setAttribute('data-theme', newTheme)
  localStorage.setItem('theme', newTheme)
})

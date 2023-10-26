/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../Rucula.FrontEnd/**/*.{html,js}"],
    theme: {
        extend: {},
    },
    plugins: [],
    ...(process.env.NODE_ENV === 'production' ? { cssnano: {} } : {})
}


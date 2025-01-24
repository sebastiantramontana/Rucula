/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../Rucula.FrontEnd/index.html"],
    theme: {
        extend: {},
    },
    plugins: [],
    ...(process.env.NODE_ENV === 'production' ? { cssnano: {} } : {})
}


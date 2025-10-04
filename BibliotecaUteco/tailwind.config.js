/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.razor",   // todos los componentes razor
        "./**/*.html",    // html estático
        "./**/*.cshtml",  // si tienes layouts compartidos
        "./**/*.cs",      // opcional: para clases en atributos
        "./wwwroot/index.html"
    ],
    theme: {
        extend: {},
    },
    plugins: [],
};

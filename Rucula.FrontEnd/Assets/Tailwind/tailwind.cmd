echo Copiar el frontend a la carpeta src!
pause
npx tailwindcss -i ./src/css/styles.css -o ./dist/styles.css --watch
echo Copiar el styles.css que quedó en /dist al frontend
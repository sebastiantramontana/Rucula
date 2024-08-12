mkdir "..\Release"
pause "publicar wasm en Release"
start archivosEstaticos.cmd
start tailwind.cmd
pause "Quitar cdn de tailwindcss!"
exit

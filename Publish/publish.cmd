mkdir "..\Release"
pause "publicar wasm en Release"
start archivosEstaticos.cmd
start tailwind.cmd
start apache.cmd
pause "Quitar cdn de tailwindcss!"
exit

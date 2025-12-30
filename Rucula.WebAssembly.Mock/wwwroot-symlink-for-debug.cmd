REM El parametro %1 es la ruta raiz de Rucula, donde se encuentra la solucion de VS, y no lleva El ultimo backslash
REM El parametro %2 es la ruta del front-end, donde se encuentra el index.html
SET wwwrootSymLinkPath=%1\Rucula.WebAssembly.Mock\wwwroot
SET frontEndPath=%2

mklink /D %wwwrootSymLinkPath% %frontEndPath%
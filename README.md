# TFG Ruben Gimbert Arm

En aquest projecte es troba l'escena 3D en la qual s'ha desenvolupat el braç amb el moviment IK en 2D. La versió de l'editor de Unity utilitzada és la 2021.3.18f1. Si intentem obrir el projecte des de Unity, ens hauria de sortir l'opció de descarregar aquesta versió. Sinó, es pot descarregar una versió igual o superior des de la pestanya Installs de Unity i obrir el projecte.  

- Si executem el joc, com per defecte el braç té activat el component animation_target, es mourà adoptant diverses configuracions de manera cíclica. Es pot desactivar des de la pestanya Inspector seleccionant l'Arm, i moure manualment l'esfera T1 per tal de moure el braç i provar diferents configuracions.
- D'altra banda, tenim un cos creat amb 2 rèpliques dels braços, i una rèplica de l'esfera T1 anomenada T1Body. Si executem i movem T1Body, el braç dret es mourà apuntant cap a aquest.

Cuando un usuario quiere agregar un futbolista a su plantilla y luego consultar el puntaje de una fecha, intervienen los objetos **Jugador**, **Plantilla**, **Usuario** y **Puntuacion**. Tambien PlantillaSuplente o PlantillaTitular si el jugador se elegio como suplente o titular de la plantilla.

Guardaria los datos que sirvan para que el jugador que eligio el usuario se quede dentro de su plantilla.

Podria salir mal si el usuario elige un jugador incorrecto, si elige una plantilla incorrecta, si ya tiene el maximo de jugadores en su plantilla, si no tiene el presupuesto requerido para agregar a ese jugador, o cuando busca el puntaje escribe una fecha incorrecta o futura.

## Hipotesis:

Las peticiones no validas, que tengan que ver con las reglas de las plantillas (maximo de jugadores, presupuesto), se van a rechazar dentro de Plantilla.

Si el usuario escribe un jugador no valido, el error va a estar en Jugador.

Si al momento de buscar puntaje por fecha de una plantilla se ingresa una fecha o plantilla incorrecta, el error va a estar en Puntuacion.

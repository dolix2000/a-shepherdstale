# Object Pool
*Scripts that are used to randomly generate, store and spawn clouds (and possibly other objects if desired).*

## Generator
The Generator is used to spawn the clouds in our scenes. Based on Lost Relics Tutorial, but changed to work with the ObjectPool.

## Cloud Spawner
Only used as a helper script for the Generator, allows the cloud-prefabs to display their current speed in the inspector.

## Object Pool Manager
If an object has a short lifespan (is instantiated and destroyed rather quickly) it might be good to use an Object Pool, where an object is pre-generated and stored, 
and only gets activated if needed. If the object is not needed anymore, it will get deactivated and return to the pool.

To run tests related to the movement got to 
Window > General > Test Runner > Play Mode Test > AAObjectPoolTest
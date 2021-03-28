using UnityEngine;

namespace Placeables {
    public class Placeable : IPlaceable {
        public GameObject gameObject;
        public Vector3 tileSize;
        public string name;
        public Vector3 rootPos;

        public Placeable(GameObject gameObject, Transform transform, string name = null) {
            this.gameObject = gameObject;
            rootPos = this.gameObject.GetComponent<Transform>().position;
            this.gameObject.GetComponent<Transform>().position = transform.position;
            this.gameObject.GetComponent<Transform>().rotation = transform.rotation;
            if (name != null) {
                this.gameObject.name = name;
            }
            this.name = this.gameObject.name;
            tileSize = this.gameObject.GetComponent<MeshRenderer>().bounds.size;
        }
        public Placeable(GameObject gameObject, Vector3 offset, string name = null) {
            this.gameObject = gameObject;
            rootPos = this.gameObject.GetComponent<Transform>().position;
            this.gameObject.GetComponent<Transform>().position += offset;
            if (name != null) {
                this.gameObject.name = name;
            }
            this.name = this.gameObject.name;
            tileSize = this.gameObject.GetComponent<MeshRenderer>().bounds.size;
        }
        public Placeable(GameObject gameObject, string name = null) {
            this.gameObject = gameObject;
            rootPos = this.gameObject.GetComponent<Transform>().position;
            if (name != null) {
                this.gameObject.name = name;
            }
            this.name = this.gameObject.name;
            tileSize = this.gameObject.GetComponent<MeshRenderer>().bounds.size;
        }

        public void MoveTo(Transform transform) {
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
        }
        public void MoveTo(Vector3 position) {
            gameObject.transform.position = position;
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        public void MoveBy(Vector3 offset) {
            gameObject.transform.position += offset;
        }

        public void RotateAround(Vector3 point, Vector3 axis, float angle) {
            this.gameObject.GetComponent<Transform>().RotateAround(point, axis, angle);
        }
        public void Rotate(float angle) {
            this.gameObject.GetComponent<Transform>().RotateAround(rootPos, Vector3.right, angle);
        }
    }

    public class Road : Placeable {
        public Road(GameObject gameObject, Transform transform, string name = null) : base(gameObject, transform, name) {
        }
        public Road(GameObject gameObject, Vector3 position, string name = null) : base(gameObject, position, name) {
        }
        public Road(GameObject gameObject, string name = null) : base(gameObject, name) {
        }
    }

    public class Building : Placeable {
        public Building(GameObject gameObject, Transform transform, string name = null) : base(gameObject, transform, name) {
        }
        public Building(GameObject gameObject, Vector3 position, string name = null) : base(gameObject, position, name) {
        }
        public Building(GameObject gameObject, string name = null) : base(gameObject, name) {
        }
    }

    public class Obstacle: Placeable {
        public Obstacle(GameObject gameObject, Transform transform, string name = null) : base(gameObject, transform, name) {
        }
        public Obstacle(GameObject gameObject, Vector3 position, string name = null) : base(gameObject, position, name) {
        }
        public Obstacle(GameObject gameObject, string name = null) : base(gameObject, name) {
        }
    }

    public class NPC: Placeable, INPC {
        public NPC(GameObject gameObject, Transform transform, string name = null) : base(gameObject, transform, name) {
        }
        public NPC(GameObject gameObject, Vector3 position, string name = null) : base(gameObject, position, name) {
        }
        public NPC(GameObject gameObject, string name = null) : base(gameObject, name) {
        }
        public void RandomWalk(Vector3 normal, float variance) {
            // TODO: Implement Box-Muller transform
            return;
        }
        public void Face(Vector3 point) {
            return;
        }
    }
}

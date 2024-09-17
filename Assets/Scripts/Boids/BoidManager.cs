using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;

    public Spawner spawner;

    void Start () { 
        foreach (Boid b in spawner.boids) {
            b.Initialize (settings, null);
        }

    }

    void Update () {
        if (spawner.boids != null) {

            int numBoids = spawner.boids.Length;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < spawner.spawnCount; i++) {
                boidData[i].position = spawner.boids[i].position;
                boidData[i].direction = spawner.boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
            boidBuffer.SetData (boidData);

            compute.SetBuffer (0, "boids", boidBuffer);
            compute.SetInt ("numBoids", spawner.boids.Length);
            compute.SetFloat ("viewRadius", settings.perceptionRadius);
            compute.SetFloat ("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt (numBoids / (float) threadGroupSize);
            compute.Dispatch (0, threadGroups, 1, 1);

            boidBuffer.GetData (boidData);

            for (int i = 0; i < spawner.spawnCount; i++) {
                spawner.boids[i].avgFlockHeading = boidData[i].flockHeading;
                spawner.boids[i].centreOfFlockmates = boidData[i].flockCentre;
                spawner.boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                spawner.boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                spawner.boids[i].UpdateBoid ();
            }

            boidBuffer.Release ();
        }
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.IO;
using Microsoft.Xna.Framework;

namespace SpiderGame
{
    public class Kinect
    {
        /// <summary>
        /// Sensor de la Kinect
        /// </summary>
        private KinectSensor sensor;


        public Vector2 MainGauche
        {
            get
            {
                return mMainGauche;
            }
        }
        private Vector2 mMainGauche;
        
        public Vector2 MainDroite     
        {
            get
            {
                return mMainDroite;
            }
        }
        private Vector2 mMainDroite;

        private static Kinect instance;

        private Skeleton[] skeletons;

        /// <summary>
        /// Constructeur complet
        /// </summary>
        private Kinect()
        {
            KinectStart();
            mMainGauche = Vector2.Zero;
            mMainDroite = Vector2.Zero;
        }

        /// <summary>
        /// Méthode permettant de gérer l'unicité de l'objet Kinect
        /// </summary>
        /// <returns></returns>
        public static Kinect getInstance()
        {
            if (instance == null)
                instance = new Kinect();
            return instance;
        }

        /// <summary>
        /// Méthode permettant d'initialiser la kinect
        /// </summary>
        public void KinectStart()
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }
        }


        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        public void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            skeletons = new Skeleton[0];
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            try
            {
                mMainGauche = SkeletonPointToScreen(skeletons.First().Joints[JointType.HandLeft].Position);
                mMainDroite = SkeletonPointToScreen(skeletons.First().Joints[JointType.HandRight].Position);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Vector2 SkeletonPointToScreen(SkeletonPoint skelpoint)
        {            
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            Point pt = new Point(depthPoint.X, depthPoint.Y);
            return new Vector2(pt.X * 1.25f, pt.Y);
        }

        /// <summary>
        /// Méthode qui va indiquer si un joueur est detecté ou non
        /// </summary>
        public bool estDetecté()
        {
            if (skeletons.First().TrackingState == SkeletonTrackingState.NotTracked)
                return false;
            else
                return true;
        }

       /// <summary>
       /// Méthode qui va arreter la Kinect
       /// </summary>
        public void KinectStop()
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

    }
}

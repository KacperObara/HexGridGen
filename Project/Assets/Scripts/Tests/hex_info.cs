﻿using NUnit.Framework;
using HexGen;
using UnityEngine;

namespace Tests
{
    public class hex_data
    {
        [Test]
        public void calculates_axial_from_localPos_1()
        {
            AxialCoordinates actual = HexData.LocalToAxial(38, 32);
            AxialCoordinates expected = new AxialCoordinates(22, 32);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_axial_from_localPos_2()
        {
            AxialCoordinates actual = HexData.LocalToAxial(17, 21);
            AxialCoordinates expected = new AxialCoordinates(7, 21);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_localPos_from_axial_1()
        {
            Vector2Int actual = HexData.AxialToLocal(22, 32);
            Vector2Int expected = new Vector2Int(38, 32);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_localPos_from_axial_2()
        {
            Vector2Int actual = HexData.AxialToLocal(7, 21);
            Vector2Int expected = new Vector2Int(17, 21);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_axial_from_cube_1()
        {
            AxialCoordinates actual = HexData.CubeToAxial(22, -54, 32);
            AxialCoordinates expected = new AxialCoordinates(22, 32);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_axial_from_cube_2()
        {
            AxialCoordinates actual = HexData.CubeToAxial(7, -28, 21);
            AxialCoordinates expected = new AxialCoordinates(7, 21);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_cube_from_axial_1()
        {
            CubeCoordinates actual = HexData.AxialToCube(22, 32);
            CubeCoordinates expected = new CubeCoordinates(22, -54, 32);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_cube_from_axial_2()
        {
            CubeCoordinates actual = HexData.AxialToCube(7, 21);
            CubeCoordinates expected = new CubeCoordinates(7, -28, 21);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_roundAxial_from_Pixel_1()
        {
            AxialCoordinates actual = HexData.AxialRound(24.95771f, 32.86159f);
            AxialCoordinates expected = new AxialCoordinates(25, 33);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void calculates_roundAxial_from_Pixel_2()
        {
            AxialCoordinates actual = HexData.AxialRound(7.03695f, 20.77667f);
            AxialCoordinates expected = new AxialCoordinates(7, 21);
            Assert.AreEqual(expected, actual);
        }
    }
}

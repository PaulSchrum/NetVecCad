using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NVcad.Foundations;
using NVcad.Foundations.Angles;
using System.Collections.Generic;

namespace UnitTestNVcad
{
   [TestClass]
   public class TestClassForCadFoundations
   {
      private Double delta = 0.0000001;

      [TestMethod]
      public void ptsDegree_sin90_returns1p0()
      {
         Degree deg = 90.0;
         Double expectedDbl = 1.0;
         Double actualDbl = Degree.Sin(deg);
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void ptsDegree_Atan2Of10And0_returns90degrees()
      {
         Degree deg = Degree.Atan2(10.0, 0.0);
         Double expectedDbl = 90.0;
         Double actualDbl = deg.getAsDouble();
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void ptsDegree_AsinOf1overSqrt2_shouldEqual45degrees()
      {
         Degree deg = Degree.Asin(1.0 / Math.Sqrt(2.0));
         Double expectedDbl = 45.0;
         Double actualDbl = deg.getAsDouble();
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void AzimuthAddition_Az189PlusDeflNeg15_shouldEqual174()
      {
         Double expectedDbl = 174.0;
         Azimuth az = new Azimuth(); az.setFromDegreesDouble(189.0);
         Deflection defl = new Deflection(); defl.setFromDegreesDouble(-15.0);
         Azimuth newAz = az + defl;
         Double actualDbl = newAz.getAsDegreesDouble();
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void ptsAngle_settingTo1_shouldResultIn_equals57_2957795Degrees()
      {
         Angle angle = 1.0;
         Double expected = 57.2957795;
         Double actual = angle.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Azimuth_setToDMS183__29__29_5_shouldResultIn_Angle()
      {
         Azimuth anAzimuth = new Azimuth();
         anAzimuth.setFromDegreesMinutesSeconds(183, 29, 29.5);
         Double expected = 183.4915277778;
         Double actual = anAzimuth.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Deflection_setTo_Pos1Rad_shouldBe_Pos1Rad()
      {
         Deflection aDefl = new Deflection();
         aDefl = (Deflection)1.0;
         Double expected = 1.0;
         Double actual = aDefl.getAsRadians();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Deflection_setTo_Neg1Rad_shouldBe_Neg1Rad()
      {
         Deflection aDefl = new Deflection();
         aDefl = (Deflection)(-1.0);
         Double expected = -1.0;
         Double actual = aDefl.getAsRadians();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Deflection_setTo_Pos6Rad_shouldBe_Pos6Rad()
      {
         Deflection aDefl = new Deflection();
         aDefl = (Deflection)6.0;
         Double expected = 6.0;
         Double actual = aDefl.getAsRadians();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Deflection_setTo_Pos2_shouldBe_Pos2Degrees()
      {
         Deflection defl = new Deflection();
         defl.setFromDegreesDouble(2.0);

         Double expected = 2.0;
         Double actual = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Deflection_setTo_neg5__18__29_5()
      {
         Deflection aDeflection = new Deflection();
         aDeflection.setFromDegreesMinutesSeconds(-5, 18, 29.5);
         Double expected = -5.308194444444;
         Double actual = aDeflection.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Azimuth1_30_addDeflection_Pos2_15_shouldYieldNewAzimuth_3_45()
      {
         Azimuth anAzimuth = new Azimuth();
         anAzimuth.setFromDegreesMinutesSeconds(1, 30, 0);
         Deflection aDefl = new Deflection();
         aDefl.setFromDegreesMinutesSeconds(2, 15, 0);

         Double expected = 3.75;
         Azimuth newAz = anAzimuth + aDefl;
         Double actual = newAz.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }


      [TestMethod]
      public void Azimuth_setFromXY()
      {
         Tuple<Double, Double, Double>[] testCases =  {
            new Tuple<Double, Double, Double>(10, 2, 78.690067526),
            new Tuple<Double, Double, Double>(10, -2, 101.309932474),
            new Tuple<Double, Double, Double>(-10, 2, 281.309932474),
            new Tuple<Double, Double, Double>(-10, -2, 258.690067526) };
         
         foreach(var testCase in testCases)
         {
            Azimuth anAzimuth = new Azimuth();
            anAzimuth.setFromXY(testCase.Item1, testCase.Item2);
            Double actualDegrees = anAzimuth.getAsDegreesDouble();

            Assert.AreEqual(expected: testCase.Item3, actual: actualDegrees, delta: delta);
         }
      }


      [TestMethod]
      public void AzimuthArithmatic_subtraction()
      {
         Tuple<Double, Double, Double>[] testCases =  {
            new Tuple<Double, Double, Double>(20.0, 10.0, -10.0),
            new Tuple<Double, Double, Double>(340.0, 350.0, 10.0),
            new Tuple<Double, Double, Double>(20.0, 340.0, -40.0),
            new Tuple<Double, Double, Double>(340.0, 20.0, 40.0) };

         foreach(var testCase in testCases)
         {
            Double Az1Dbl = testCase.Item1; 
            Double Az2Dbl = testCase.Item2;
            Double expectedDeflection = testCase.Item3;
            Azimuth Az1 = new Azimuth(); Az1.setFromDegreesDouble(Az1Dbl);
            Azimuth Az2 = new Azimuth(); Az2.setFromDegreesDouble(Az2Dbl);

            Double actualDeflection = Az2.minus(Az1).getAsDegreesDouble();

            Assert.AreEqual(expected: expectedDeflection, actual: actualDeflection, delta: 0.00000001);
         }
      }


      [TestMethod]
      public void AzimuthArithmatic_addition()
      {
         Tuple<Double, Double, Double>[] testCases =  {
            new Tuple<Double, Double, Double>(20.0, 10.0, -10.0),
            new Tuple<Double, Double, Double>(340.0, 350.0, 10.0),
            new Tuple<Double, Double, Double>(20.0, 340.0, -40.0),
            new Tuple<Double, Double, Double>(340.0, 20.0, 40.0),
            new Tuple<Double, Double, Double>(189.4326, 173.8145, -15.6181), };

         foreach (var testCase in testCases)
         {
            Double Az1Dbl = testCase.Item1;
            Double ExpectedAz2Dbl = testCase.Item2;
            Double DeflectionDbl = testCase.Item3;
            Azimuth Az1 = new Azimuth(); Az1.setFromDegreesDouble(Az1Dbl);
            Deflection defl = DeflectionDbl.AsPtsDegree();
            Azimuth Az2 = Az1 + defl;

            Double actualAzimuth = Az2.getAsDegreesDouble();

            Assert.AreEqual(expected: ExpectedAz2Dbl, actual: actualAzimuth, delta: 0.00000001);
         }
      }


      [TestMethod]
      public void ComputeRemainder_ScaledByDenominator()
      {
         Tuple<Double, Double, Double>[] testCases =  {
            new Tuple<Double, Double, Double>(5.0, 10.0, 5.0),
            new Tuple<Double, Double, Double>(15.0, 10.0, 5.0),
            new Tuple<Double, Double, Double>(-5.0, 10.0, -5.0),
            new Tuple<Double, Double, Double>(-15.0, 10.0, -5.0), };

         foreach (var testCase in testCases)
         {
            Double numerator = testCase.Item1;
            Double Denominator = testCase.Item2;
            Double expectedDbl = testCase.Item3;
            Double actualDbl = Angle.ComputeRemainderScaledByDenominator(numerator, Denominator);
            Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: 0.00000001);
         }
      }


      [TestMethod]
      public void Slope_from100PercentByConstructor_is45Degrees()
      {

         Slope slope = new Slope(1.0);
         Double expected = 45.0;

         Double actual = slope.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: 0.00001);
         //slope = 
      }

      [TestMethod]
      public void Slope_IsVerticalUpward()
      {
         var slope = new Slope();
         slope.setFromXY(0.0, 1.0);

         Assert.IsTrue(slope.isVertical());

         Assert.AreEqual(expected: "Vertical", actual: slope.ToString());
         
         Assert.IsTrue(slope.isSlopeUp());
      }
   }
}

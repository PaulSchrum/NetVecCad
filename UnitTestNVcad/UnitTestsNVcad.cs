using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NVcad.Foundations;
using NVcad.Foundations.Angles;
using System.Collections.Generic;
using NVcad.Foundations.Coordinates;
using NVcad.Foundations.WorkingUnits;
using NVcad.CadObjects;
using System.Media;
using NVcad.Foundations.Symbology;
using NVcad.Models;
using System.IO;
using System.Linq;

namespace UnitTestNVcad
{
   [TestClass]
   public class TestClassForCadFoundations
   {
      private Double delta = 0.0000001;

      [TestMethod]
      public void Degree_sin90_returns1p0()
      {
         Degree deg = 90.0;
         Double expectedDbl = 1.0;
         Double actualDbl = Degree.Sin(deg);
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void Degree_Atan2Of10And0_returns90degrees()
      {
         Degree deg = Degree.Atan2(10.0, 0.0);
         Double expectedDbl = 90.0;
         Double actualDbl = deg.getAsDouble();
         Assert.AreEqual(expected: expectedDbl, actual: actualDbl, delta: delta);
      }

      [TestMethod]
      public void Degree_AsinOf1overSqrt2_shouldEqual45degrees()
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
      public void Angle_cos_minus120_equalsMinus0_5()
      {
         Double expected = -0.5;
         Double actual = (Angle.AngleFactory(-120.0)).cos();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_sin_minus150_equalsMinus0_5()
      {
         Double expected = -0.5;
         Double actual = (Angle.AngleFactory(-150.0)).sin();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_tan_minus45_equalsMinus1()
      {
         Double expected = -1.0;
         Double actual = (Angle.AngleFactory(-45.0)).tan();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_fromDxDy_quadrant1_1010_equals45Degrees()
      {
         Double expected = 45.0;
         Double actual = (Angle.AngleFactory(dx: 10.0, dy: 10.0)).getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_fromDxDy_quadrant1_1010_equals135Degrees()
      {
         Double expected = 135.0;
         Double actual = (Angle.AngleFactory(dx: -10.0, dy: 10.0)).getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_fromDxDy_quadrant1_1010_equalsMinus135Degrees()
      {
         Double expected = -135.0;
         Double actual = (Angle.AngleFactory(dx: -10.0, dy: -10.0)).getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void Angle_fromDxDy_quadrant1_1010_equalsMinus45Degrees()
      {
         Double expected = -45.0;
         Double actual = (Angle.AngleFactory(dx: 10.0, dy: -10.0)).getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
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
      public void DeflectionRight_divideBy2_isCorrect()
      {
         Deflection deflA = new Deflection();
         deflA.setFromDegreesDouble(4.0);
         Deflection defl = deflA / 2.0;

         Double expected = 2.0;
         Double actual = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expected, actual: actual, delta: delta);
      }

      [TestMethod]
      public void DeflectionLeft_divideBy2_isCorrect()
      {
         Deflection deflA = new Deflection();
         deflA.setFromDegreesDouble(-4.0);
         Deflection defl = deflA / 2.0;

         Double expected = -2.0;
         Double actual = defl.getAsDegreesDouble();
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

      [TestMethod]
      public void Point2D_plusVector_equalsCorrectPoint()
      {
         Point pt1 = new Point(10.0, 10.0);
         Vector vec1 = new Vector(5.0, 5.0, 5.0);

         Point pt2 = pt1 + vec1;

         Double expectedX = 15.0; Double expectedY = 15.0;

         Assert.AreEqual(expected: expectedX, actual: pt2.x, delta: 0.00001);
         Assert.AreEqual(expected: expectedY, actual: pt2.y, delta: 0.00001);
      }

      [TestMethod]
      public void Vector_newVector_isCorrect()
      {
         var aVec = new Vector(Azimuth.ctorAzimuthFromDegree(90.0),
            50.0);
         Assert.IsNotNull(aVec);
         Assert.AreEqual(expected: 50.0,
            actual: aVec.x, delta: 0.00001);
      }

      [TestMethod]
      public void Vector_rotating1_1vector90degress_yields1_Minus1vectorOfSameLength()
      {
         Vector firstVec = new Vector(1.0, 1.0, null);
         var firstAngle = firstVec.DirectionHorizontal;
         Vector secondVec = firstVec.rotateCloneAboutZ(Angle.AngleFactory(-90.0));
         Assert.AreEqual(expected: firstVec.Length, actual: secondVec.Length, delta: 0.00001);
         Assert.AreEqual(expected: firstVec.Azimuth.getAsDegreesDouble() + 90.0, actual: secondVec.Azimuth.getAsDegreesDouble(), delta: 0.00001);
      }

      [TestMethod]
      public void Vector_rotatingM1_M1vector90degress_yieldsM1_1vectorOfSameLength()
      {
         Vector firstVec = new Vector(-1.0, -1.0, null);
         var firstAngle = firstVec.DirectionHorizontal;
         Vector secondVec = firstVec.rotateCloneAboutZ(Angle.AngleFactory(-90.0));
         Assert.AreEqual(expected: firstVec.Length, actual: secondVec.Length, delta: 0.00001);
         Assert.AreEqual(expected: firstVec.Azimuth.getAsDegreesDouble() + 90.0, actual: secondVec.Azimuth.getAsDegreesDouble(), delta: 0.00001);
      }

      [TestMethod]
      public void angleNormalization_withinPlusOrMinus2Pi_OverPositive2PI()
      {
         Double angleNeedingToBeNormalized = 2 * Math.PI * 4.56;
         Double expectedAfterNormalized = 2 * Math.PI * 0.56;
         Angle anAngle = new Angle();
         Double actualAfterNormalization =
            Angle.ComputeRemainderScaledByDenominator(angleNeedingToBeNormalized, 2 * Math.PI);

         Assert.AreEqual(expected: expectedAfterNormalized,
            actual: actualAfterNormalization, delta: 0.0000001);
      }

      [TestMethod]
      public void angleNormalization_withinPlusOrMinus2Pi_UnderNegative2PI()
      {
         Double angleNeedingToBeNormalized = -710.0;
         Double expectedAfterNormalized = -350.0;
         Angle anAngle = new Angle();
         Double actualAfterNormalization =
            Angle.ComputeRemainderScaledByDenominator(angleNeedingToBeNormalized, 360.0);

         Assert.AreEqual(expected: expectedAfterNormalized,
            actual: actualAfterNormalization, delta: 0.0000001);
      }

      [TestMethod]
      public void Deflection_positiveLessThan180_getAsDegrees()
      {
         Double expectedValue = 45.0;
         Deflection defl = new Deflection(0.785398164, 1);
         Double actualValue = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_positiveGreaterThan180_getAsDegrees()
      {
         Double expectedValue = 310.0;
         Deflection defl = new Deflection(5.41052068118, 1);
         Double actualValue = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_negativeLessThan180_getAsDegrees()
      {
         Double expectedValue = -45.0;
         Deflection defl = new Deflection(0.785398164, -1);
         Double actualValue = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_negativeGreaterThan180_getAsDegrees()
      {
         Double expectedValue = -310.0;
         Deflection defl = new Deflection(5.41052068118, -1);
         Double actualValue = defl.getAsDegreesDouble();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_positiveLessThan180_getAsRadians()
      {
         Double expectedValue = 0.785398164;
         Deflection defl = new Deflection(0.785398164, 1);
         Double actualValue = defl.getAsRadians();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_positiveGreaterThan180_getAsRadians()
      {
         Double expectedValue = 5.41052068118;
         Deflection defl = new Deflection(5.41052068118, 1);
         Double actualValue = defl.getAsRadians();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void Deflection_negativeLessThan180_getAsRadians()
      {
         Double expectedValue = -0.39479111970;
         Azimuth begAz = new Azimuth(new Point(0.0, 0.0, 0.0), new Point(10.0, 50.0, 0.0));
         Azimuth endAz = new Azimuth(new Point(10.0, 50.0, 0.0), new Point(0.0, 100.0, 0.0));
         Deflection defl = new Deflection(begAz, endAz, true);
         Double actualValue = defl.getAsRadians();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.0000001);
      }

      [TestMethod]
      public void Deflection_negativeGreaterThan180_getAsRadians()
      {
         Double expectedValue = -5.88839418748;
         Azimuth endAz = new Azimuth(new Point(0.0, 0.0, 0.0), new Point(10.0, 50.0, 0.0));
         Azimuth begAz = new Azimuth(new Point(10.0, 50.0, 0.0), new Point(0.0, 100.0, 0.0));
         Deflection defl = new Deflection(begAz, endAz, false);
         Double actualValue = defl.getAsRadians();
         Assert.AreEqual(expected: expectedValue, actual: actualValue, delta: 0.00001);
      }

      [TestMethod]
      public void rayGetElevationInRayDomain_IsCorrect()
      {
         Ray localRay = new Ray();
         localRay.StartPoint = new Point(25.0, 0.0, 25.0);
         localRay.Slope = new Slope(1.0);
         localRay.HorizontalDirection = null;
         double? actual = localRay.getElevationAlong(35.0);
         double? expected = 35.0;
         Assert.AreEqual(expected.ToString(), actual.ToString());
         //String act = actual.ToString();
         //String exp = expected.ToString();
      }

      [TestMethod]
      public void rayGetElevationOutsideRayDomain_IsNull()
      {
         Ray localRay = new Ray();
         localRay.StartPoint = new Point(25.0, 0.0, 25.0);
         localRay.Slope = new Slope();
         localRay.Slope.setFromXY(0, 1);
         localRay.HorizontalDirection = null;
         double? actual = localRay.getElevationAlong(25.0);
         Assert.IsNull(actual);
      }

      [TestMethod]
      public void rayGetElevationWhenRayIsVertical_IsNull()
      {
         Ray localRay = new Ray();
         localRay.StartPoint = new Point(25.0, 0.0, 25.0);
         localRay.Slope = new Slope(1.0);
         localRay.HorizontalDirection = null;
         double? actual = localRay.getElevationAlong(15.0);
         Assert.IsNull(actual);
      }

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_20_20Rotated45degrees_yields28_28()
      {
         var bb = new BoundingBox(-10, -10, 10, 10);
         var aspectVector = bb.getAsVectorLLtoUR();
         bb.setFrom_2dOnly(new Point(0, 0), 20, 20, Angle.AngleFactory(45.0));
         aspectVector = bb.getAsVectorLLtoUR();

         Double expectedURx = 14.142136; Double expectedURy = 14.142136;
         Double actualURx = bb.upperRightPt.x;
         Double actualURy = bb.upperRightPt.y;

         Assert.AreEqual(expected: expectedURx, actual: actualURx, delta: 0.00001);
         Assert.AreEqual(expected: expectedURy, actual: actualURy, delta: 0.00001);
         Assert.AreEqual(expected: aspectVector.Length, actual: 40.0, delta: 0.00001);
         Assert.AreEqual(expected: aspectVector.Azimuth.getAsDegreesDouble(),
            actual: 45.0, delta: 0.00001);
      }

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_Translated_20_20Rotated45degrees_yields28_28()
      {
         var bb = new BoundingBox(-90, -10, 110, 10);
         var aspectVector = bb.getAsVectorLLtoUR();
         bb.setFrom_2dOnly(new Point(100, 0), 20, 20, Angle.AngleFactory(45.0));
         aspectVector = bb.getAsVectorLLtoUR();

         Double expectedURx = 114.142136; Double expectedURy = 14.142136;
         Double expectedLLx = 85.85786;
         Double actualURx = bb.upperRightPt.x;
         Double actualURy = bb.upperRightPt.y;
         Double actualLLx = bb.lowerLeftPt.x;

         Assert.AreEqual(expected: expectedURx, actual: actualURx, delta: 0.00001);
         Assert.AreEqual(expected: expectedLLx, actual: actualLLx, delta: 0.00001);
         Assert.AreEqual(expected: expectedURy, actual: actualURy, delta: 0.00001);
         Assert.AreEqual(expected: aspectVector.Length, actual: 40.0, delta: 0.00001);
         Assert.AreEqual(expected: aspectVector.Azimuth.getAsDegreesDouble(),
            actual: 45.0, delta: 0.00001);
      }


      //[TestCategory("BoundingBox"), TestMethod()]
      //public void BoundingBox_constructorValuesKatiwumpus_BBstillValid()

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_OverlapsOther_Partial_returnsTrue()
      {
         var bbLeftLow = new BoundingBox(100, 100, 200, 200);
         var bbRightHigh = new BoundingBox(150, 150, 250, 250);
         Assert.AreEqual(expected: true,
            actual: bbLeftLow.overlapsWith(bbRightHigh));
         Assert.AreEqual(expected: true,
            actual: bbRightHigh.overlapsWith(bbLeftLow));
      }

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_NoOverlapOther_returnsFalse()
      {
         var bbLeftLow = new BoundingBox(100, 100, 150, 150);
         var bbRightHigh = new BoundingBox(151, 151, 250, 250);
         Assert.AreEqual(expected: false,
            actual: bbLeftLow.overlapsWith(bbRightHigh));
         Assert.AreEqual(expected: false,
            actual: bbRightHigh.overlapsWith(bbLeftLow));
      }

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_TotallyContainsOther_returnsTrue()
      {
         var bbBigUn = new BoundingBox(100, 100, 500, 500);
         var bbLittleUn = new BoundingBox(150, 150, 250, 250);
         Assert.AreEqual(expected: true,
            actual: bbBigUn.overlapsWith(bbLittleUn));
         Assert.AreEqual(expected: true,
            actual: bbLittleUn.overlapsWith(bbBigUn));
      }

      [TestCategory("BoundingBox"), TestMethod()]
      public void BoundingBox_CenterPoint_returnCorrectValue()
      {
         var aBB = new BoundingBox(-100, -100, 100, 100);
         Assert.AreEqual(expected: 0.0, actual: aBB.centerPt.x, delta: 0.000001);
         Assert.AreEqual(expected: 0.0, actual: aBB.centerPt.y, delta: 0.000001);
      }

      [TestCategory("WorkingUnits"), TestMethod()]
      public void UnitsLength_1Inch_equals_0p0254Meters()
      {
         Double expected = 0.0254;
         var length = new Length(1.0, LengthUnit.Inch);
         Double actual = length.GetAs(LengthUnit.Meter);
         Assert.AreEqual(expected: expected, actual: actual, delta: 0.000000001);
      }

      [TestCategory("WorkingUnits"), TestMethod()]
      public void UnitsLength_12Inch_equals_1Foot()
      {
         Double expected = 1.0;
         var length = new Length(12.0, LengthUnit.Inch);
         Double actual = length.GetAs(LengthUnit.Foot);
         Assert.AreEqual(expected: expected, actual: actual, delta: 0.000000001);
      }

   }

   [TestClass]
   public class TestClassForCadObject
   {


      [TestMethod]
      public void Arc_CreateArc_ctor3DeflectingLeft90_IsCorrect()
      {
         var anArc = new Arc(new Point(100.0, 100.0),
            Azimuth.ctorAzimuthFromDegree(90),
            Deflection.ctorDeflectionFromAngle(90.0, -1), 50.0);
         Assert.IsNotNull(anArc);

         Assert.AreEqual(
            expected: 180.0,
            actual: anArc.BeginRadiusVector.Azimuth.getAsDegreesDouble(),
            delta: 0.00001, message: "BeginRadiusVector");
         Assert.AreEqual(
            expected: -45.0,
            actual: anArc.BeginPointAngle.getAsDegreesDouble(),
            delta: 0.00001, message: "BeginPointAngle");
         Assert.IsNotNull(anArc.BoundingBox);
         Assertt.AreEqual(
            expected: new Point(100,150),
            actual: anArc.CenterPt);
         Assertt.AreEqual(
            expected: new Vector(Azimuth.ctorAzimuthFromDegree(135.0), 50.0),
            actual: anArc.CentralVector);
         Assert.AreEqual(
            expected: -90.0,
            actual: anArc.Deflection.getAsDegreesDouble(),
            delta: 0.00001, message: "Deflection");
         Assert.AreEqual(
            expected: 0.0,
            actual: anArc.Eccentricity,
            delta: 0.00001, message: "Eccentricity");
         Assertt.AreEqual(
            expected: new Point(150.0, 150.0),
            actual: anArc.EndPoint);
         var expecVec = new Vector(Azimuth.ctorAzimuthFromDegree(90.0),
            50.0);
         Assertt.AreEqual(
            expected: expecVec,
            actual: anArc.EndRadiusVector);
         Assertt.AreEqual(
            expected: new Point(100.0, 100.0),
            actual: anArc.Origin);
         Assert.AreEqual(
            expected: 50.0,
            actual: anArc.Radius,
            delta: 0.00001, message: "Radius");
         Assert.AreEqual(
            expected: 90.0,
            actual: anArc.Rotation.getAsDegreesDouble(),
            delta: 0.00001, message: "Rotation");
         Assertt.AreEqual(
            expected: new Vector(1.0,1.0),
            actual: anArc.ScaleVector);
      }

      [TestMethod]
      public void Arc_CreateArc_ctor3DeflectingRight90_IsCorrect()
      {
         var anArc = new Arc(new Point(100.0, 100.0),
            Azimuth.ctorAzimuthFromDegree(90),
            Deflection.ctorDeflectionFromAngle(90.0, 1), 50.0);

         Assert.IsNotNull(anArc);

         Assert.AreEqual(
            expected: 0.0,
            actual: anArc.BeginRadiusVector.Azimuth.getAsDegreesDouble(),
            delta: 0.00001);
         Assert.AreEqual(
            expected: 45.0,
            actual: anArc.BeginPointAngle.getAsDegreesDouble(),
            delta: 0.00001);
         Assert.IsNotNull(anArc.BoundingBox);
         Assertt.AreEqual(
            expected: new Point(100,50),
            actual: anArc.CenterPt);
         Assertt.AreEqual(
            expected: new Vector(Azimuth.ctorAzimuthFromDegree(45.0), 50.0),
            actual: anArc.CentralVector);
         Assert.AreEqual(
            expected: 90.0,
            actual: anArc.Deflection.getAsDegreesDouble(),
            delta: 0.00001);
         Assert.AreEqual(
            expected: 0.0,
            actual: anArc.Eccentricity,
            delta: 0.00001);
         Assertt.AreEqual(
            expected: new Point(150.0, 50.0),
            actual: anArc.EndPoint);
         var expecVec = new Vector(Azimuth.ctorAzimuthFromDegree(90.0),
            50.0);
         Assertt.AreEqual(
            expected: expecVec,
            actual: anArc.EndRadiusVector);
         Assertt.AreEqual(
            expected: new Point(100.0, 100.0),
            actual: anArc.Origin);
         Assert.AreEqual(
            expected: 50.0,
            actual: anArc.Radius,
            delta: 0.00001);
         Assert.AreEqual(
            expected: 90.0,
            actual: anArc.Rotation.getAsDegreesDouble(),
            delta: 0.00001);
         Assertt.AreEqual(
            expected: new Vector(1.0,1.0),
            actual: anArc.ScaleVector);
      }

      [TestMethod]
      public void FeatureList_NewHasDefaultFeature()
      {
         FeatureList fl = new FeatureList();
         Assert.AreEqual(expected: 1,
            actual: fl.Children.Count);
         Assert.AreEqual(expected: "Default",
            actual: fl.Children["Default"].Name);
      }

      private void add3Features(FeatureList aFL)
      {
         var f1 = new Feature();
         f1.Name = "F1";
         aFL.AddFeature(f1);
         f1 = new Feature();
         f1.Name = "F2";
         aFL.AddFeature(f1);
         f1 = new Feature();
         f1.Name = "F3";
         aFL.AddFeature(f1);
      }

      [TestMethod]
      public void FeatureList_Adding3Features_yields4Count()
      {
         FeatureList fl = new FeatureList();
         add3Features(fl);
         Assert.AreEqual(expected: 4,
            actual: fl.Children.Count);
      }

      [TestMethod]
      public void FeatureList_RemovingDefault_Fails()
      {
         FeatureList fl = new FeatureList();
         add3Features(fl);
         int precount = fl.Children.Count;
         bool actual = fl.TryRemoveFeature("Default");
         Assert.AreEqual(expected: false,
            actual: actual);
         Assert.AreEqual(expected: precount,
            actual: fl.Children.Count);
      }

      [TestMethod]
      public void FeatureList_RemovingExistingMember_Succeeds()
      {
         FeatureList fl = new FeatureList();
         add3Features(fl);
         int precount = fl.Children.Count;
         bool actual = fl.TryRemoveFeature("F2");
         Assert.AreEqual(expected: true,
            actual: actual);
         Assert.AreEqual(expected: precount - 1,
            actual: fl.Children.Count);
      }

      [TestMethod]
      public void FeatureList_RemovingNonExistantMember_Fails()
      {
         FeatureList fl = new FeatureList();
         add3Features(fl);
         int precount = fl.Children.Count;
         bool actual = fl.TryRemoveFeature("Doesnt Exist");
         Assert.AreEqual(expected: false,
            actual: actual);
         Assert.AreEqual(expected: precount,
            actual: fl.Children.Count);
      }

      private void createABlankModel()
      {
         if(null == aModel)
         {
            aModel = new Model();
            dxfFileName_NCDOT_Dsn = @"..\..\..\TestDataSets\NCDOT_B4656\B4656_RDY_DSN.dxf";
         }
      }

      private Model aModel { get; set; }
      private String dxfFileName_NCDOT_Dsn { get; set; }
      private String pathTest { get; set; }

      [TestMethod]
      public void DXF_readingNCDOTfile_has59Features()
      {
         createABlankModel();
         aModel.LoadDXFFile(dxfFileName_NCDOT_Dsn);
         var numberOfFeatures = aModel.FeatureList.Children.Count;

         Assert.AreEqual(expected: 59,
            actual: numberOfFeatures);
      }

      [TestMethod]
      public void DXF_readingNCDOTfile_has295Arcs()
      {
         createABlankModel();
         aModel.LoadDXFFile(dxfFileName_NCDOT_Dsn);
         var graphicElements = aModel.allGrahics;
         int numberOfArcs = graphicElements.Where(element => element is Arc).Count();

         Assert.AreEqual(expected: 295,
            actual: numberOfArcs);
      }

   }

   public static class Assertt
   {
      public static void AreEqual(Vector expected, Vector actual)
      {
         Assert.AreEqual(expected: expected.x, actual: actual.x, delta: 0.0001);
         Assert.AreEqual(expected: expected.y, actual: actual.y, delta: 0.0001);

         if (expected.z != null)
         {
            if (actual.z != null)
            {
               Assert.AreEqual(
                  expected: (Double)expected.z,
                  actual: (Double)actual.z,
                  delta: 0.0001);
            }
            else
            {
               Assert.Fail("One of two Nullable Doubles is null.");
            }
         }
         else if (actual.z != null)
         {
            Assert.Fail("One of two Nullable Doubles is null.");
         }
      }

      public static void AreEqual(Point expected, Point actual)
      {
         Assert.AreEqual(expected: expected.x, actual: actual.x, delta: 0.0001);
         Assert.AreEqual(expected: expected.y, actual: actual.y, delta: 0.0001);

         if (expected.z != null)
         {
            if (actual.z != null)
            {
               Assert.AreEqual(
                  expected: (Double)expected.z,
                  actual: (Double)actual.z,
                  delta: 0.0001);
            }
            else
            {
               Assert.Fail("One of two Nullable Doubles is null.");
            }
         }
         else if (actual.z != null)
         {
            Assert.Fail("One of two Nullable Doubles is null.");
         }
      }

   }

}

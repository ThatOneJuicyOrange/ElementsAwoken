using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ElementsAwoken
{
    public class BezierCurve // Bezier Curve Method, made by Seraph
    {
		public Vector2[] Controls; // the points used for the "verticies" of the curver
		public BezierCurve(params Vector2[] controlPoints)
		{
			Controls = controlPoints;
		}

		public BezierCurve(List<Vector2> controlPoints)
		{
			Vector2[] points = new Vector2[controlPoints.Count];
			for (int i = 0; i < controlPoints.Count; ++i)
			{
				points[i] = controlPoints[i];
			}
			Controls = points;
		}



		private Vector2 EvaluateRecursive(Vector2[] controls, float distance)
		{
			if (controls.Length <= 2)
			{

				return Vector2.Lerp(controls[0], controls[1], distance);

			}
			else
			{
				Vector2[] nextPoints = new Vector2[controls.Length - 1];

				for (int i = 0; i < controls.Length - 1; ++i)
				{
					nextPoints[i] = Vector2.Lerp(controls[i], controls[i + 1], distance);
				}

				return this.EvaluateRecursive(nextPoints, distance);
			}

			
		}

		public Vector2 GetSinglePoint(float proportionalDistance)
		{
			if (proportionalDistance > 1f)
			{
				proportionalDistance = 1f;
			}
			if (proportionalDistance < 0f)
			{
				proportionalDistance = 0f;
			}
			
			return this.EvaluateRecursive(this.Controls, proportionalDistance);
		}

		public List<Vector2> GetPoints(int amount)//returns a list
		{
			float interval = 1f / (float)amount;
			
			
			List<Vector2> points = new List<Vector2>();
			
			for (float i = 0f; i <= 1f; i += interval)
			{
				points.Add(GetSinglePoint(i));
			}

			return points;
		}

        // broken (i think) dont use
		public Vector2[] GetPointsAlternate(int amount)//returns an array
		{
			float interval = 1f / (float)amount;


			Vector2[] points = new Vector2[amount];

			for (float i = 0f; i <= 1f; i += interval)
			{
				int index = (int)((float)i / interval);
				points[index] = GetSinglePoint(i);
			}

			return points;
		}


	}
}

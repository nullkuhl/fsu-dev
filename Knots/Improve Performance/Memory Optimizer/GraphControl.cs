using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryOptimizer
{
	/// <summary>
	/// Used to initialize the graph Control
	/// </summary>
	public struct GraphStruct
	{
		/// <summary>
		/// number of points on X axis
		/// </summary>
		public int PointsOnXAxis;
		/// <summary>
		/// number of points on Y axis
		/// </summary>
		public int PointsOnYAxis;
		/// <summary>
		/// start value for X axis
		/// </summary>
		public float StartValueXAxis;
		/// <summary>
		/// start value for Y axis
		/// </summary>
		public float StartValueYAxis;
		/// <summary>
		/// increments in which values should be laid out on X-Axis
		/// </summary>
		public int StepXAxis;
 		/// <summary>
		/// increments in which values should be laid out on Y-Axis
 		/// </summary>
		public int StepYAxis;

		/// <summary>
		/// GraphStruct constructor
		/// </summary>
		/// <param name="pX">pX</param>
		/// <param name="pY">pY</param>
		/// <param name="startValX">x start value</param>
		/// <param name="startValY">y start value</param>
		/// <param name="stepX">x step</param>
		/// <param name="stepY">y step</param>
		public GraphStruct(int pX, int pY, float startValX, float startValY, int stepX, int stepY)
		{
			PointsOnXAxis = pX;
			PointsOnYAxis = pY;
			StartValueXAxis = startValX;
			StartValueYAxis = startValY;
			StepXAxis = stepX;
			StepYAxis = stepY;
		}
	}

	/// <summary>
	/// Used to store a point's coordinates in float
	/// </summary>
	public struct FloatPoint
	{
		/// <summary>
		/// display value of this point
		/// </summary>
		public float Value;
		/// <summary>
		/// x coordinate
		/// </summary>
		public float X;
		/// <summary>
		/// y coordinate
		/// </summary>
		public float Y;

		/// <summary>
		/// FloatPoint constructor
		/// </summary>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		public FloatPoint(float x, float y)
		{
			X = x;
			Y = y;
			Value = 0;
		}
	}

	/// <summary>
	/// Enumeration for style of graph LineGraph or BarGraph
	/// </summary>
	public enum GraphType
	{
		Line,
		Bar
	};

	/// <summary>
	/// GraphControl
	/// </summary>
	public sealed class GraphControl : UserControl
	{
		readonly ArrayList points; //list to store Display points
		readonly ArrayList graphPointsX; //list to store X,Y coordinates of graph
		readonly ArrayList graphPointsY; //list to store X,Y coordinates of graph
		/// <summary>
		/// GraphStruct
		/// </summary>
		public GraphStruct GRAPHSTRUCT;
		Color axesColor; //setthe display ,axes or graph color
		bool bShowPoints; //show the display points on graph
		Color graphColor; //setthe display ,axes or graph color
		Region graphRegion; //region to draw the graph on
		GraphType graphType; //type of graph
		FloatPoint origin;
		Color textColor; //setthe display ,axes or graph color
		FloatPoint xAxis;
		string xTitle; // titles for X and Y axes
		FloatPoint yAxis;
		string yTitle; // titles for X and Y axes

		/// <summary>
		/// GraphControl constructor
		/// </summary>
		public GraphControl()
		{
			//set the defualt values
			xTitle = "None";
			yTitle = "None";
			graphType = GraphType.Line;
			textColor = Color.Blue;
			axesColor = Color.Red;
			graphColor = Color.Green;
			BackColor = Color.AliceBlue;
			//GraphStruct is set to have 10 pts on each axes starting value is 1 
			//and step increment is also 1 by defualt.
			GRAPHSTRUCT = new GraphStruct(10, 10, 1, 1, 1, 1);
			points = new ArrayList();
			graphPointsX = new ArrayList();
			graphPointsY = new ArrayList();
		}

		/// <summary>
		/// Text color
		/// </summary>
		public Color TextColor
		{
			get { return textColor; }
			set { textColor = value; }
		}

		/// <summary>
		/// Axes color
		/// </summary>
		public Color AxesColor
		{
			get { return axesColor; }
			set { axesColor = value; }
		}

		/// <summary>
		/// Graph color
		/// </summary>
		public Color GraphColor
		{
			get { return graphColor; }
			set { graphColor = value; }
		}

		/// <summary>
		/// x axis title
		/// </summary>
		public string TitleXAxis
		{
			get { return xTitle; }
			set { xTitle = value; }
		}

		/// <summary>
		/// y axis title
		/// </summary>
		public string TitleYAxis
		{
			get { return yTitle; }
			set { yTitle = value; }
		}

		/// <summary>
		/// Is points shown on the graph
		/// </summary>
		public bool ShowPointsOnGraph
		{
			get { return bShowPoints; }
			set { bShowPoints = value; }
		}

		/// <summary>
		/// Graph type
		/// </summary>
		public GraphType GraphStyle
		{
			get { return graphType; }
			set { graphType = value; }
		}

		/// <summary>
		/// AddPoints to graph
		/// </summary>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		public void AddPoints(int x, int y)
		{
			points.Add(new Point(x, y));
		}

		/// <summary>
		/// AddPoints to graph
		/// </summary>
		/// <param name="x">x points array</param>
		/// <param name="y">y points array</param>
		public void AddPoints(int[] x, int[] y)
		{
			for (int i = 0; i < x.Length; i++)
			{
				points.Add(new Point(x[i], y[i]));
			}
		}

		/// <summary>
		/// AddPoints to graph
		/// </summary>
		/// <param name="pts">x,y points array</param>
		public void AddPoints(int[,] pts)
		{
			for (int i = 0; i < pts.GetLength(0); i++)
			{
				points.Add(new Point(pts[i, 0], pts[i, 1]));
			}
		}

		/// <summary>
		/// AddPoints to graph
		/// </summary>
		/// <param name="pt">point</param>
		public void AddPoints(Point pt)
		{
			points.Add(pt);
		}

		/// <summary>
		/// AddPoints to graph
		/// </summary>
		/// <param name="pts">points</param>
		public void AddPoints(Point[] pts)
		{
			foreach (Point t in pts)
			{
				points.Add(t);
			}
		}

		//draws the X and Y axes and also set the origin and x and y axes points
		//This method should be called before any other drawing method.
		void DrawAxes(Graphics g)
		{
			//get the bounds of our region.We will draw within the region
			RectangleF rect = Region.GetBounds(g);
			float xOrigin = rect.Left + 20;
			float yOrigin = rect.Bottom - 70;

			origin = new FloatPoint(xOrigin, yOrigin);
			xAxis = new FloatPoint(rect.Right - 20, origin.Y);
			yAxis = new FloatPoint(origin.X, rect.Top);

			var axisPen = new Pen(axesColor);

			g.DrawLine(axisPen, origin.X, origin.Y, xAxis.X, xAxis.Y);
			g.DrawLine(axisPen, origin.X, origin.Y, yAxis.X, yAxis.Y);

			axisPen.Dispose();
		}

		//draws the co-ordinates on x and y axes.
		void DrawPoints(Graphics g)
		{
			float xDiff = xAxis.X - origin.X;
			float yDiff = origin.Y - yAxis.Y;
			float xStep = xDiff/GRAPHSTRUCT.PointsOnXAxis;
			float yStep = yDiff/GRAPHSTRUCT.PointsOnYAxis;

			var fpt = new FloatPoint(origin.X, origin.Y) {Value = 0};
			graphPointsX.Add(fpt);
			graphPointsY.Add(fpt);

			var p = new Pen(textColor);
			Brush b = new SolidBrush(textColor);
			var f = new Font(Font.FontFamily, Font.Size);
			for (int i = 1; i <= GRAPHSTRUCT.PointsOnXAxis; i++)
			{
				float xAxisX = origin.X + (i*xStep);
				float xAxisY = origin.Y;

				g.DrawLine(p, xAxisX, xAxisY - 2, xAxisX, xAxisY + 2);
				float val = GRAPHSTRUCT.StartValueXAxis + ((i - 1)*GRAPHSTRUCT.StepXAxis);
				g.DrawString(val.ToString(), f, b, xAxisX - 5, xAxisY + 3);

				fpt.X = xAxisX;
				fpt.Y = 0;
				fpt.Value = val;
				graphPointsX.Add(fpt);
			}
			for (int j = 1; j <= GRAPHSTRUCT.PointsOnYAxis; j++)
			{
				float yAxisX = origin.X;
				float yAxisY = origin.Y - (j*yStep);

				g.DrawLine(p, yAxisX - 2, yAxisY, yAxisX + 2, yAxisY);
				float val = GRAPHSTRUCT.StartValueYAxis + ((j - 1)*GRAPHSTRUCT.StepYAxis);
				g.DrawString(val.ToString(), f, b, yAxisX - 15, yAxisY);

				fpt.X = 0;
				fpt.Y = yAxisY;
				fpt.Value = val;
				graphPointsY.Add(fpt);
			}
			f.Dispose();
			b.Dispose();
			p.Dispose();
		}

		//draws the actual graph, Line style.
		void DrawLineGraph(Graphics g)
		{
			var p = new Pen(graphColor);
			var start = new Point();
			if (points.Count > 0)
				start = (Point) points[0];

			FloatPoint prev = FindLocationOnGraph(start);
			for (int i = 1; i < points.Count; i++)
			{
				var pt = (Point) points[i];
				FloatPoint current = FindLocationOnGraph(pt);
				g.DrawLine(p, prev.X, prev.Y, current.X, current.Y);
				if (bShowPoints)
				{
					Brush b = new SolidBrush(textColor);
					var f = new Font(Font.FontFamily, Font.Size);
					string title = "(" + pt.X + "," + pt.Y + ")";
					if (prev.Y > current.Y)
						g.DrawString(title, f, b, current.X - 25, current.Y - 10);
					else
						g.DrawString(title, f, b, current.X - 25, current.Y + 2);
					if (i == 1)
					{
						title = "(" + start.X + "," + start.Y + ")";
						g.DrawString(title, f, b, prev.X - 10, prev.Y - 15);
					}
					f.Dispose();
					b.Dispose();
				}
				prev = current;
			}
			p.Dispose();
		}

		//draws the actual graph ,Bar style
		void DrawBarGraph(Graphics g)
		{
			var p = new Pen(graphColor);
			foreach (object t in points)
			{
				var pt = (Point) t;
				FloatPoint current = FindLocationOnGraph(pt);
				g.DrawLine(p, current.X - 1, origin.Y, current.X - 1, current.Y);
				g.DrawLine(p, current.X - 1, current.Y, current.X + 1, current.Y);
				g.DrawLine(p, current.X + 1, current.Y, current.X + 1, origin.Y);
				if (bShowPoints)
				{
					Brush b = new SolidBrush(textColor);
					var f = new Font(Font.FontFamily, Font.Size);
					string title = "(" + pt.X + "," + pt.Y + ")";
					g.DrawString(title, f, b, current.X - 25, current.Y - 15);
					f.Dispose();
					b.Dispose();
				}
			}
			p.Dispose();
		}

		//draws titles for X and Y axes.
		void DrawTitles(Graphics g)
		{
			Brush b = new SolidBrush(textColor);
			var f = new Font(Font.FontFamily, Font.Size);
			string title = "X-Axis = " + xTitle + "    " + "Y-Axis = " + yTitle;
			g.DrawString(title, f, b, origin.X, origin.Y + 15);
			f.Dispose();
			b.Dispose();
		}

		//Given a display point, this function will point the actual co-ordinates
		//for this point on our graph. It acts like the ScreenToClient function in Windows
		FloatPoint FindLocationOnGraph(Point pt)
		{
			float diffValue, finalYValue;
			float finalXValue = finalYValue = 0;
			for (int i = 0; i < graphPointsX.Count; i++)
			{
				//store the current point
				var current = (FloatPoint) graphPointsX[i];
				//if points X is lesser that current points Value
				if (pt.X < current.Value)
				{
					var previous = (FloatPoint) graphPointsX[i - 1];
					//store diff between current X and previous X coordinate
					float diffX = current.X - previous.X;
					//store difference between values of current and prev points
					diffValue = current.Value - previous.Value;
					float unitsPerCoordinate = diffValue/diffX;
					finalXValue = ((pt.X - previous.Value)/unitsPerCoordinate) + previous.X;
					break;
				}
				if (pt.X == current.Value)
				{
					finalXValue = current.X;
				}
			}
			for (int j = 0; j < graphPointsY.Count; j++)
			{
				var current = (FloatPoint) graphPointsY[j];
				if (pt.Y < current.Value)
				{
					var previous = (FloatPoint) graphPointsY[j - 1];
					float diffY = current.Y - previous.Y;
					diffValue = current.Value - previous.Value;
					float unitsPerCoordinate = diffValue/diffY;
					finalYValue = ((pt.Y - previous.Value)/unitsPerCoordinate) + previous.Y;
					break;
				}
				if (pt.Y == current.Value)
				{
					finalYValue = current.Y;
				}
			}
			var fpNew = new FloatPoint(finalXValue, finalYValue);
			return fpNew;
		}

		//overridden to paint our graph
		protected override void OnPaint(PaintEventArgs pe)
		{
			var bound = new Rectangle(new Point(0, 0), Size);
			graphRegion = new Region(bound);
			Region = graphRegion;
			DrawAxes(pe.Graphics);
			DrawPoints(pe.Graphics);
			DrawTitles(pe.Graphics);
			if (graphType == GraphType.Bar)
				DrawBarGraph(pe.Graphics);
			else
				DrawLineGraph(pe.Graphics);
			//drawAxes is called twice becuase the axes get overwwiten sometimes by other lines
			DrawAxes(pe.Graphics);
		}
	}
}
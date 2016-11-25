//// Copyright 2016-2017 ?????????????. All Rights Reserved.
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;

//namespace VisualScriptTool.GDIHelper
//{
//	public class DiagramTab : TabPage
//	{
//		private const string KEY_POSITION = "P";
//		private const string KEY_ON_ENTER = "Ent";
//		private const string KEY_ON_UPDATE = "Upd";
//		private const string KEY_ON_EXIT = "Ext";
//		private const string KEY_ENTER_STATE = "EnS";
//		private const string KEY_EXIT_STATE = "ExS";
//		private const string KEY_STATES = "S";
//		private const string KEY_TRANSITION_FROM = "F";
//		private const string KEY_TRANSITION_TO = "T";
//		private const string KEY_TRANSITION_CONDITION = "C";
//		private const string KEY_TRANSITION_EVENTS = "E";
//		private const string KEY_TRANSITION_EVENT_NAMESPACE = "NS";
//		private const string KEY_TRANSITION_EVENT_NAME = "N";
//		private const string KEY_TRANSITIONS = "T";

//		private StateDiagram diagram = null;
//		private bool isDirty = false;

//		private ToolStripTextBox TBName = null;
//		private ToolStripComboBox CBGame = null;

//		public int GameID
//		{
//			get { return (int)CBGame.ComboBox.SelectedValue; }
//			private set { CBGame.ComboBox.SelectedValue = value; }
//		}

//		public int ID
//		{
//			get;
//			set;
//		}

//		public bool IsDirty
//		{
//			get { return isDirty; }
//			set
//			{
//				isDirty = value;

//				UpdateTabText();
//			}
//		}

//		public DiagramTab(int GameID, int ID, string Name)
//		{
//			diagram = new StateDiagram();
//			diagram.BackColor = Color.FromArgb(73, 73, 73);
//			diagram.BorderStyle = BorderStyle.FixedSingle;
//			//diagram.Debug = false;
//			//diagram.DefaultColor = Color.FromArgb(255, 128, 0);
//			diagram.Dock = DockStyle.Fill;
//			diagram.DrawGrid = true;
//			diagram.DrawShadow = true;
//			//diagram.FillColor = Color.FromArgb(40, 166, 122);
//			//diagram.FilterText = "";
//			diagram.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
//			//diagram.FSM_Events = new Event[0];
//			diagram.GridImage = Resources.GridPattern;
//			diagram.Location = new Point(3, 20);
//			diagram.PanX = 0F;
//			diagram.PanY = 0F;
//			diagram.ShadowThickness = 31;
//			//diagram.ShowParentRelation = false;
//			//diagram.ShowTransition = true;
//			diagram.TabIndex = 0;
//			//diagram.Tool = StateDiagram.Tools.Move;
//			diagram.Zoom = 1.0F;

//			Controls.Add(diagram);

//			TBName = new ToolStripTextBox();
//			TBName.Size = new Size(300, 23);
//			TBName.Text = Name;
//			TBName.TextChanged += NameChanged;

//			CBGame = new ToolStripComboBox();
//			CBGame.ComboBox.DisplayMember = "Name";
//			CBGame.ComboBox.ValueMember = "ID";
//			CBGame.ComboBox.DataSource = DataLayer.GetGames();
//			CBGame.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

//			MenuStrip menu = new MenuStrip();
//			Controls.Add(menu);
//			menu.Items.AddRange(new ToolStripItem[] { CBGame, TBName });

//			diagram.SomethingChanged += SomethingChanged;

//			this.GameID = GameID;
//			this.ID = ID;
//			this.Name = Name;
//			Text = Name;
//		}

//		public void New()
//		{
//			diagram.RootState = new State();
//			diagram.RootState.Name = Name;

//			diagram.RootState.MakeFSM();

//			diagram.LookAt(diagram.RootState.Position);

//			IsDirty = true;
//		}

//		public void Load(string Definition)
//		{
//			if (string.IsNullOrEmpty(Definition))
//				return;

//			JsonObject obj = (JsonObject)SimpleJSON.DeserializeObject(Definition);

//			diagram.RootState = new State();
//			diagram.RootState.Name = Name;

//			AddJSONToState(diagram.RootState, obj);
//		}

//		public string Save()
//		{
//			JsonObject obj = new JsonObject();

//			if (!AddStateToJSON(obj, diagram.RootState))
//				return string.Empty;

//			return obj.ToString();
//		}

//		public string Export()
//		{
//			DiagramExporterCSharp exporter = new DiagramExporterCSharp(diagram.RootState);
//			return exporter.Export();
//		}

//		private static void SetStateAttributes(State State, JsonObject Data)
//		{
//			string pos = Data[KEY_POSITION].ToString();
//			string[] parts = pos.Remove(pos.Length - 1).Substring(1).Split(',');

//			State.Position = new PointF(float.Parse(parts[0].Split('=')[1]), float.Parse(parts[1].Split('=')[1]));

//			object data = Data[KEY_ON_ENTER];
//			if (data != null)
//				State.OnEnter = data.ToString();

//			data = Data[KEY_ON_UPDATE];
//			if (data != null)
//				State.OnUpdate = data.ToString();

//			data = Data[KEY_ON_EXIT];
//			if (data != null)
//				State.OnExit = data.ToString();
//		}

//		private static void AddJSONToState(State Parent, JsonObject Data)
//		{
//			if (!Data.ContainsKey(KEY_ENTER_STATE))
//				return;

//			Parent.MakeFSM();

//			JsonObject obj = (JsonObject)Data[KEY_ENTER_STATE];
//			SetStateAttributes(Parent.EnterState, obj);
//			AddJSONToState(Parent.EnterState, obj);

//			obj = (JsonObject)Data[KEY_EXIT_STATE];
//			SetStateAttributes(Parent.ExitState, obj);
//			AddJSONToState(Parent.ExitState, obj);

//			JsonObject statesObj = (JsonObject)Data[KEY_STATES];

//			IEnumerator<KeyValuePair<string, object>> it = statesObj.GetEnumerator();
//			while (it.MoveNext())
//			{
//				State state = new State();
//				Parent.AddState(state);

//				state.Name = it.Current.Key;

//				obj = (JsonObject)it.Current.Value;

//				SetStateAttributes(state, obj);
//				AddJSONToState(state, obj);
//			}

//			JsonObject transitionsObj = (JsonObject)Data[KEY_TRANSITIONS];
//			it = transitionsObj.GetEnumerator();
//			while (it.MoveNext())
//			{
//				obj = (JsonObject)it.Current.Value;

//				Transition transition = new Transition(Parent.Get(obj[KEY_TRANSITION_FROM].ToString()), Parent.Get(obj[KEY_TRANSITION_TO].ToString()));
//				transition.Condition = obj[KEY_TRANSITION_CONDITION].ToString();

//				if (obj.ContainsKey(KEY_TRANSITION_EVENTS))
//				{
//					JsonObject eventsObj = (JsonObject)obj[KEY_TRANSITION_EVENTS];

//					transition.Events = new Transition.EventInfo[eventsObj.Count];

//					for (int i = 0; i < transition.Events.Length; ++i)
//					{
//						JsonObject eventInfoObj = (JsonObject)eventsObj[i.ToString()];
//						transition.Events[i] = new Transition.EventInfo(eventInfoObj[KEY_TRANSITION_EVENT_NAMESPACE].ToString(), eventInfoObj[KEY_TRANSITION_EVENT_NAME].ToString());
//					}
//				}

//				Parent.AddTransition(transition);
//			}
//		}

//		private static JsonObject CreateJsonFromState(State State)
//		{
//			JsonObject stateObj = new JsonObject();

//			stateObj[KEY_POSITION] = State.Position.ToString();
//			stateObj[KEY_ON_ENTER] = State.OnEnter;
//			stateObj[KEY_ON_UPDATE] = State.OnUpdate;
//			stateObj[KEY_ON_EXIT] = State.OnExit;

//			if (State.IsFSM)
//				AddStateToJSON(stateObj, State);

//			return stateObj;
//		}

//		private static bool AddStateToJSON(JsonObject Parent, State State)
//		{
//			if (State.IsFSM)
//			{
//				Parent[KEY_ENTER_STATE] = CreateJsonFromState(State.EnterState);
//				Parent[KEY_EXIT_STATE] = CreateJsonFromState(State.ExitState);
//			}

//			JsonObject statesObj = new JsonObject();

//			for (int i = 0; i < State.States.Length; ++i)
//			{
//				State state = State.States[i];

//				if (state.IsRequired)
//					continue;

//				if (statesObj.ContainsKey(state.Name))
//				{
//					MessageBox.Show("Two or more state with name [" + state.Name + "] exists in graph, cannot Save and/or Export");
//					return false;
//				}

//				statesObj[state.Name] = CreateJsonFromState(state);
//			}

//			Parent[KEY_STATES] = statesObj;

//			JsonObject transitonsObj = new JsonObject();

//			for (int i = 0; i < State.Transitions.Length; ++i)
//			{
//				Transition transition = State.Transitions[i];

//				JsonObject transitionObj = new JsonObject();

//				transitionObj[KEY_TRANSITION_FROM] = transition.From.Name;
//				transitionObj[KEY_TRANSITION_TO] = transition.To.Name;
//				transitionObj[KEY_TRANSITION_CONDITION] = transition.Condition;

//				if (transition.Events != null)
//				{
//					JsonObject eventsObj = new JsonObject();

//					for (int j = 0; j < transition.Events.Length;++j)
//					{
//						Transition.EventInfo eventInfo = transition.Events[j];
//						JsonObject eventInfoObj = new JsonObject();

//						eventInfoObj[KEY_TRANSITION_EVENT_NAMESPACE] = eventInfo.NameSpace;
//						eventInfoObj[KEY_TRANSITION_EVENT_NAME] = eventInfo.EventName;

//						eventsObj[j.ToString()] = eventInfoObj;
//					}

//					transitionObj[KEY_TRANSITION_EVENTS] = eventsObj;
//				}

//				transitonsObj[i.ToString()] = transitionObj;
//			}

//			Parent[KEY_TRANSITIONS] = transitonsObj;

//			return true;
//		}

//		private void SomethingChanged(object sender, System.EventArgs e)
//		{
//			IsDirty = true;
//		}

//		private void NameChanged(object sender, EventArgs e)
//		{
//			Name = TBName.Text;
//			UpdateTabText();
//		}

//		private void UpdateTabText()
//		{
//			if (isDirty)
//				Text = Name + "*";
//			else
//				Text = Name;
//		}
//	}
//}
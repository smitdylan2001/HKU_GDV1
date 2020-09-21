using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
	/// <summary> This list keeps track of all the simple keycommands </summary>
	private List<KeyCommand>	   _keyCommands = new List<KeyCommand>();
	/// <summary> This list keeps track of all the keycommands that need to come from a point of origin </summary>
	private List<KeyOriginCommand> _keyOriginCommands = new List<KeyOriginCommand>();
	/// <summary> This list keeps track of all the keycommands that need to come from a point of origin as an GetButtonDown input </summary>
	private List<KeyOriginCommand> _keyOriginCommandsDown = new List<KeyOriginCommand>();


	/// <summary>
	/// This method is responsible for 
	/// </summary>
	public void HandleInput()
	{
		//Input onkeydown without origin
		foreach (var keyCommand in _keyCommands)
		{
			if (Input.GetKeyDown(keyCommand._key))
			{
				keyCommand._command.Execute();
			}
		}
		//Input onkeydown with origin
		foreach (var keyCommand in _keyOriginCommands)
		{
			if (Input.GetKeyDown(keyCommand._key))
			{
				keyCommand._command.Execute(keyCommand._origin);
			}
		}
		//Input onkey with origin
		foreach (var keyCommand in _keyOriginCommandsDown)
		{
			if (Input.GetKey(keyCommand._key))
			{
				keyCommand._command.Execute(keyCommand._origin);
			}
		}
	}

	#region InputWithoutCommand
	/// <summary>
	/// This binds a key to a command
	/// </summary>
	/// <param name="keyCode"></param>
	/// <param name="command"></param>
	public void BindInputToCommand(KeyCode keyCode, ICommand command)
	{
		_keyCommands.Add(new KeyCommand()
		{
			_key = keyCode,
			_command = command
		});
	}
	/// <summary>
	/// This unbinds a key from a command
	/// </summary>
	/// <param name="keyCode"></param>
	public void UnbindInput(KeyCode keyCode)
	{
		var items = _keyCommands.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyCommands.Remove(x));
	}
	#endregion

	#region InputWithCommand
	/// <summary>
	/// This binds a key to a command with a point of origin
	/// </summary>
	/// <param name="keyCode"></param>
	/// <param name="command"></param>
	/// <param name="origin"></param>
	public void BindInputToCommandWithOrigin(KeyCode keyCode, IGameObjectCommand command, GameObject origin)
	{
		_keyOriginCommands.Add(new KeyOriginCommand()
		{
			_key = keyCode,
			_command = command,
			_origin = origin
		});
	}

	/// <summary>
	/// Unbinds an input with origin from a command
	/// </summary>
	/// <param name="keyCode"></param>
	public void UnbindInputWithOrigin(KeyCode keyCode)
	{
		var items = _keyOriginCommands.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyOriginCommands.Remove(x));
	}
	#endregion

	#region InputWithCommandDown
	/// <summary>
	/// binds an input with an origin to a command for OnButtonDown input
	/// </summary>
	/// <param name="keyCode"></param>
	/// <param name="command"></param>
	/// <param name="origin"></param>
	public void BindInputToCommandWithOriginDown(KeyCode keyCode, IGameObjectCommand command, GameObject origin)
	{
		_keyOriginCommandsDown.Add(new KeyOriginCommand()
		{
			_key = keyCode,
			_command = command,
			_origin = origin
		});
	}
	/// <summary>
	/// unbinds an input with an origin to a command for OnButtonDown input
	/// </summary>
	/// <param name="keyCode"></param>
	public void UnbindInputWithOriginDown(KeyCode keyCode)
	{
		var items = _keyOriginCommandsDown.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyOriginCommandsDown.Remove(x));
	}

	#endregion
}

/// <summary>
/// This is a simple class that keeps a key and a command saved
/// </summary>
public class KeyCommand
{
	public KeyCode _key;
	public ICommand _command;
}

/// <summary>
/// This is a simple class that keeps a key, command and point of origin saved.
/// </summary>
public class KeyOriginCommand
{
	public KeyCode _key;
	public IGameObjectCommand _command;
	public GameObject _origin;
}
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
	private List<KeyCommand>	   _keyCommands = new List<KeyCommand>();
	private List<KeyOriginCommand> _keyOriginCommands = new List<KeyOriginCommand>();
	private List<KeyOriginCommand> _keyOriginCommandsDown = new List<KeyOriginCommand>();

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
	public void BindInputToCommand(KeyCode keyCode, ICommand command)
	{
		_keyCommands.Add(new KeyCommand()
		{
			_key = keyCode,
			_command = command
		});
	}

	public void UnbindInput(KeyCode keyCode)
	{
		var items = _keyCommands.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyCommands.Remove(x));
	}
	#endregion

	#region InputWithCommand
	public void BindInputToCommandWithOrigin(KeyCode keyCode, IGameObjectCommand command, GameObject origin)
	{
		_keyOriginCommands.Add(new KeyOriginCommand()
		{
			_key = keyCode,
			_command = command,
			_origin = origin
		});
	}

	public void UnbindInputWithOrigin(KeyCode keyCode)
	{
		var items = _keyOriginCommands.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyOriginCommands.Remove(x));
	}
	#endregion

	#region InputWithCommandDown
	public void BindInputToCommandWithOriginDown(KeyCode keyCode, IGameObjectCommand command, GameObject origin)
	{
		_keyOriginCommandsDown.Add(new KeyOriginCommand()
		{
			_key = keyCode,
			_command = command,
			_origin = origin
		});
	}

	public void UnbindInputWithOriginDown(KeyCode keyCode)
	{
		var items = _keyOriginCommandsDown.FindAll(x => x._key == keyCode);
		items.ForEach(x => _keyOriginCommandsDown.Remove(x));
	}

	#endregion
}

public class KeyCommand
{
	public KeyCode _key;
	public ICommand _command;
}
public class KeyOriginCommand
{
	public KeyCode _key;
	public IGameObjectCommand _command;
	public GameObject _origin;
}
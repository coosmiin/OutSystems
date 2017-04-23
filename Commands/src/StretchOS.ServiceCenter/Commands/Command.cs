using System;
using System.IO;

namespace StretchOS.ServiceCenter.Commands
{
	public abstract class Command
	{
		protected string[] Parameters { get; private set; }

		public Command(TextWriter textWriter, params string[] parameters)
		{
			Parameters = parameters;
			Validate(textWriter);
		}

		public abstract void Execute();

		protected abstract bool IsValid();
		protected abstract string GetDescription();

		private void Validate(TextWriter textWriter)
		{	
			if (!IsValid())
			{
				textWriter.Write(GetDescription());
				// TODO: exception driven development is a bad practice - rewrite it with a command runner
				throw new Exception("Stop");
			}
		}
	}
}

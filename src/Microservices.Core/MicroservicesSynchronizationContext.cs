using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class MicroserviceTaskScheduler : TaskScheduler
	{
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			throw new NotImplementedException();
		}

		protected override void QueueTask(Task task)
		{
			throw new NotImplementedException();
		}

		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			throw new NotImplementedException();
		}
	}
}
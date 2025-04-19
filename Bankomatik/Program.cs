using Bankomatik.Actions.Actions;
using Bankomatik.Pipeline.Implementations;

var pipelineService = new PipelineService();
var pipeline = await pipelineService.CreatePipelineAsync();

await pipeline.AddAction<DetectUSBDrivesAction>()
	.AddAction<ChooseDriveToActWithAction>()
	.AddSubAction<ChooseDriveToActWithAction, ClearConsoleAction>()
	.AddAction<ChooseActionOnDriveAction>()
	.AddSubAction<ChooseActionOnDriveAction, CreateBankCardAction>()
	.AddSubAction<ChooseActionOnDriveAction, WithdrawMoneyAction>()
	.AddSubAction<ChooseActionOnDriveAction, DeleteBankCardAction>()
	.AddAction<ClearConsoleAction>()
	.StartAsync();

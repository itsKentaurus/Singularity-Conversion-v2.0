//
// Script name: IObserver
//
//
// Programmer: Kentaurus
//

public interface IObserver
{
	void OnNotify(ISubject subject, params object[] args);
}

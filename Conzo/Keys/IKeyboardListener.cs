namespace Conzo.Keys
{
   internal interface IKeyboardListener
   {
      event KeyboardListener.KeyPressedHandler KeyPressed;
      void Start();
      void Stop();
   }
}

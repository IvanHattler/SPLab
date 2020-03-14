.assembly DividerUint
{
  .ver 1:0:0:0
}
.module DividerUint.dll
.class public abstract auto ansi sealed beforefieldinit DividerUint.Divider 
	extends [mscorlib]System.Object
{
  .method public hidebysig static uint32 Div(uint32 a, uint32 b) cil managed
  {
    .maxstack 2
	ldarg.0
	ldarg.1		//загрузка параметров в стек
	div			//st(1)/st(0) , результат является возвращаемым значением
	ret
  }
}
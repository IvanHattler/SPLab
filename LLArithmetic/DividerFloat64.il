.assembly DividerFloat64
{
  .ver 1:0:0:0
}
.module DividerFloat64.dll
.class public abstract auto ansi sealed beforefieldinit DividerFloat64.Divider 
	extends [mscorlib]System.Object
{
  .method public hidebysig static float64  Div(float64 one,
                                               float64 two) cil managed
	{
	.maxstack  2
	ldarg.0
	ldarg.1		//загрузка параметров в стек
	div			//деление
	ret			//возврат значения
	} 

}
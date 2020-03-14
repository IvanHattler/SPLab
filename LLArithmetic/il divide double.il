.assembly LLArithmetic
{
  .ver 1:0:0:0
}
.module LLArithmetic.dll
.class public abstract auto ansi sealed beforefieldinit LLArithmetic.Divider 
	extends [mscorlib]System.Object
{
  .method public hidebysig static float64  'div'(float64 one,
                                               float64 two) cil managed
	{
	.maxstack  2
	.locals init ([0] float64 V_0)
	ldarg.0
	ldarg.1		//загрузка параметров в стек
	div			//деление
	stloc.0
	br.s      
	ldloc.0
	ret			//возврат значения
	} 

}
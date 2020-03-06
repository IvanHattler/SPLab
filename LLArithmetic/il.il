.assembly LLArithmetic
{
  .ver 1:0:0:0
}
.module LLArithmetic.dll
.class public abstract auto ansi sealed beforefieldinit LLArithmetic.Summator 
	extends [mscorlib]System.Object
{
  .method public hidebysig static int32  Sum(int32 a, int32 b) cil managed
  {
    .maxstack 8
	.locals init (int32 V_0)
	ldarg.0
	ldarg.1		;загрузка параметров в стек
	add			;сложение в стеке
	stloc.0		;загрузка в локальную переменную
	ldloc.0		;загрузка переменной в стек как выходное значение
	ret
  }
}
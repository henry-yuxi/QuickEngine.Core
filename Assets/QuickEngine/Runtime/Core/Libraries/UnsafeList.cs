using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
unsafe struct UnsafeListData
{
    public ulong arrayPtr;
    public int length;
    public int capacity;
    public uint structSize;
    public const int SIZE = 20;
}
public unsafe struct UnsafeList<T> where T : struct
{
    private UnsafeListData* data;
    public int length
    {
        get
        {
            return data->length;
        }
    }

    public int capacity
    {
        get
        {
            return data->capacity;
        }
    }

    public UnsafeList(int capacity = 10)
    {
        if (capacity < 1) capacity = 1;
        data = (UnsafeListData*)UnsafeUtility.Malloc(UnsafeListData.SIZE, 16, Allocator.Persistent);
        data->structSize = (uint)UnsafeUtility.SizeOf<T>();
        data->capacity = capacity;
        data->length = 0;
        data->arrayPtr = (ulong)UnsafeUtility.Malloc(data->structSize * data->capacity, 16, Allocator.Persistent);
    }

    private void Resize()
    {
        int newCapacity = data->capacity * 2;
        void* newArr = UnsafeUtility.Malloc(data->structSize * newCapacity, 16, Allocator.Persistent);
        UnsafeUtility.MemCpy(newArr, (void*)data->arrayPtr, data->structSize * data->capacity);
        UnsafeUtility.Free((void*)data->arrayPtr, Allocator.Persistent);
        data->arrayPtr = (ulong)newArr;
        data->capacity = newCapacity;
    }

    public void Add(ref T value)
    {
        int lastElement = data->length++;
        if (data->length > data->capacity) Resize();
        void* valueAddress = UnsafeUtility.AddressOf<T>(ref value);
        void* arrAddress = (void*)(data->arrayPtr + (ulong)(lastElement * data->structSize));
        UnsafeUtility.MemCpy(arrAddress, valueAddress, data->structSize);
    }

    public void Add(T value)
    {
        int lastElement = data->length++;
        if (data->length >= data->capacity) Resize();
        void* valueAddress = UnsafeUtility.AddressOf<T>(ref value);
        void* arrAddress = (void*)(data->arrayPtr + (ulong)(lastElement * data->structSize));
        UnsafeUtility.MemCpy(arrAddress, valueAddress, data->structSize);
    }

    public void Remove(int index)
    {
        if(index >= data->length - 1)
        {
            data->length--;
            return;
        }
        void* destination = (void*)(data->arrayPtr + (ulong)(index * data->structSize));
        void* source = (void*)((ulong)destination + data->structSize);
        UnsafeUtility.MemMove(destination, source, (data->length - index - 1) * data->structSize);
        data->length--;
    }

    public void* this[int index]
    {
        get
        {
            return (void*)(data->arrayPtr + (ulong)(index * data->structSize));
        }
        set
        {
            UnsafeUtility.MemCpy((void*)(data->arrayPtr + (ulong)(index * data->structSize)), value, data->structSize);
        }
    }

    public void Set(ref T value, int index)
    {
        void* targetIndexPointer = (void*)(data->arrayPtr + (ulong)(index * data->structSize));
        void* sourcePointer = UnsafeUtility.AddressOf<T>(ref value);
        UnsafeUtility.MemCpy(targetIndexPointer, sourcePointer, data->structSize);
    }

    public void Set(T value, int index)
    {
        void* targetIndexPointer = (void*)(data->arrayPtr + (ulong)(index * data->structSize));
        void* sourcePointer = UnsafeUtility.AddressOf<T>(ref value);
        UnsafeUtility.MemCpy(targetIndexPointer, sourcePointer, data->structSize);
    }

    public void Clear()
    {
        data->length = 0;
    }

    public void Get(ref T value, int index)
    {
        void* sourcePointer = (void*)(data->arrayPtr + (ulong)(index * data->structSize));
        void* targetPointer = UnsafeUtility.AddressOf<T>(ref value);
        UnsafeUtility.MemCpy(targetPointer, sourcePointer, data->structSize);
    }

    public void Dispose()
    {
        UnsafeUtility.Free((void*)data->arrayPtr, Allocator.Persistent);
        UnsafeUtility.Free(data, Allocator.Persistent);
    }
}
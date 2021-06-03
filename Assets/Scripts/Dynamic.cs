using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task
{
    public int num;
    public int start;
    public Task(int size)
    {
        num = size;
    }
}


public class Dynamic : MonoBehaviour
{
    public Button next;
    public Button restart;
    public Text hint;
    public Text free;
    public Button firstFitButton;
    public Button bestFitButton;
    bool firstFit = true;
    public int count = 0;
    readonly string[] nextStep = {"作业1申请130K","作业2申请60K","作业3申请100k","作业2释放60K","作业4申请200K",
        "作业3释放100K","作业1释放130K","作业5申请140K","作业6申请60K","作业7申请50K","作业6释放60K","已结束"};
    readonly Task[] tasks = { new Task(13), new Task(6), new Task(10), new Task(20), new Task(14), new Task(6), new Task(5) };
    readonly Color[] colors = { Color.cyan, Color.green, Color.grey, Color.red, Color.yellow, Color.blue, Color.magenta };

    public GameObject memoryArea;
    public List<Image> memory;
    void Start()
    {
        next.interactable = false;
        restart.interactable = false;
        next.onClick.AddListener(DoNext);
        restart.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Dynamic");
        });
        SetHint(nextStep[0]);
        Image[] images = memoryArea.GetComponentsInChildren<Image>();
        int i = 0;
        foreach (Image image in images)
        {
            memory.Add(image);
            image.GetComponentInChildren<Text>().text = (i * 10).ToString();
            i++;
        }
        firstFitButton.onClick.AddListener(delegate ()
        {
            firstFit = true;
            bestFitButton.gameObject.SetActive(false);
            firstFitButton.interactable = false;
            next.interactable = true;
            restart.interactable = true;
        });
        bestFitButton.onClick.AddListener(delegate ()
        {
            firstFit = false;
            firstFitButton.gameObject.SetActive(false);
            bestFitButton.interactable = false;
            next.interactable = true;
            restart.interactable = true;
        });
        SetFree();
    }


    void SetHint(string str)
    {
        hint.text = "下一步：" + str;
    }

    void DoNext()
    {
        switch (count)
        {
            case 0:
                Apply(0);
                break;
            case 1:
                Apply(1);
                break;
            case 2:
                Apply(2);
                break;
            case 3:
                Finish(1);
                break;
            case 4:
                Apply(3);
                break;
            case 5:
                Finish(2);
                break;
            case 6:
                Finish(0);
                break;
            case 7:
                Apply(4);
                break;
            case 8:
                Apply(5);
                break;
            case 9:
                Apply(6);
                break;
            case 10:
                Finish(5);
                next.interactable = false;
                break;
            default:
                break;
        }
        SetHint(nextStep[++count]);
        SetFree();
    }

    public void Apply(int index)
    {
        int size = tasks[index].num;
        int start;
        if (firstFit)
        {
            start = GetArea(size);
        }
        else
        {
            start = GetFitArea(size);
        }
        Debug.Log(start);
        if (start >= 0)
        {
            tasks[index].start = start;
            for (int i = start; i < start + size; i++)
            {
                memory[i].color = colors[index];
            }
        }
        else
        {
            return;
        }
    }

    public int GetArea(int size)
    {
        int start = 0;
        int num = 0;
        for (int i = 0; i < memory.Count; i++)
        {
            if (memory[i].color == Color.white)
            {
                if (num == 0)
                {
                    start = i;
                }
                num++;
            }
            else
            {
                num = 0;
            }
            if (num == size)
            {
                return start;
            }
        }
        return -1;
    }

    public int GetFitArea(int size)
    {
        List<int> start = new List<int>();
        List<int> compare = new List<int>();
        int num = 0;
        int temp = 0;
        for (int i = 0; i < memory.Count; i++)
        {
            if (memory[i].color == Color.white)
            {
                if (num == 0)
                {
                    temp = i;
                }
                num++;
            }
            else
            {
                if (num >= size)
                {
                    start.Add(temp);
                    compare.Add(num);
                    num = 0;
                }
                else
                {
                    num = 0;
                }
            }
        }
        if(num>=size)
        {
            start.Add(temp);
            compare.Add(num);
        }
        if (start.Count == 0)
        {
            return -1;
        }
        int min = 0;
        for (int i = 1; i < compare.Count; i++)
        {
            if (compare[i] < compare[min])
            {
                min = i;
            }
        }
        return start[min];
    }
    public void Finish(int index)
    {
        for (int i = tasks[index].start; i < tasks[index].start + tasks[index].num; i++)
        {
            memory[i].color = Color.white;
        }
    }

    void SetFree()
    {
        free.text = "当前空闲的空间为：\n";
        int start = 0;
        bool flag = false;
        for (int i = 0; i < memory.Count; i++)
        {
            if (memory[i].color == Color.white)
            {
                if (!flag)
                {
                    start = 10 * i;
                    flag = true;
                }
            }
            else
            {
                if (flag)
                {
                    free.text += start.ToString() + "K-" + (i * 10).ToString() + "K\n";
                    flag = false;
                }
            }
        }
        if (flag)
        {
            free.text += start.ToString() + "K-" + (memory.Count * 10).ToString() + "K\n";
        }
    }
}

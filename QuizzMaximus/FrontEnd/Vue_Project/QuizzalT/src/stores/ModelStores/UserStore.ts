import { defineStore } from 'pinia'
import type { IUser } from '@/models/'
import { UserApi } from "@/api/";
import { useDashboardStore, useAuthStore } from '@/stores/';

export const useUserStore = defineStore({
  id: 'user',
  state: () => ({
    currentUser: {} as IUser,
    users: [] as IUser[]
  }),
  getters: {
    getCurrentUser: (state) => state.currentUser,
    getUsers: (state) => state.users,
  },
  actions: {
    // Store methods - gets and sets
    setCurrentUser(user: IUser): void {
      this.currentUser = user;
    },
    setUsers(users: IUser[]): void {
      this.users = users
    },
    /**
    * Returns the stored user with the given id. 
    */
    getUserById(userId: number): IUser {
      return this.users.find((u) => u.userId == userId) as IUser;
    },
    /**
    * Returns the stored username of the given userId. 
    */
    getUserNameById(userId: number): string {
      return this.users.find((u) => u.userId == userId)?.username as string;
    },
    getUserPhotoSrc(userId: number): string {
      let userPhotoString = this.users.find(u => u.userId == userId)?.photo;
      if (userPhotoString != undefined) {
        let photo = userPhotoString?.toString();
        console.log(photo)
        return `data:image/png;base64,${userPhotoString}`
      }
      else {
        return `data:image/png;base64,${"/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAAA8AAD/7gAOQWRvYmUAZMAAAAAB/9sAhAAGBAQEBQQGBQUGCQYFBgkLCAYGCAsMCgoLCgoMEAwMDAwMDBAMDg8QDw4MExMUFBMTHBsbGxwfHx8fHx8fHx8fAQcHBw0MDRgQEBgaFREVGh8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx//wAARCAD6APoDAREAAhEBAxEB/8QApwABAAICAwEAAAAAAAAAAAAAAAMEBQYCBwgBAQEBAQEBAQEAAAAAAAAAAAAAAgEDBAUGEAABAwMBBAUGCQgHCQEAAAABAAIDEQQFBiExEgdBUWFxE4GRoSIyCLHB0UJScpIjFGKCorIzQ1MW4cLSczSUFWOTs9MkRHQ1RVURAQEAAgEDAwMEAgMBAAAAAAABAgMRMRIEIVFhQRQFcSIyE4GR8KFCI//aAAwDAQACEQMRAD8A9UoCAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgweoNc6P06D/reYtbF+/wAKWVvinujFXnzK8deWXSMuUjR773luV1s8sguLu+p862tZKHuMvhLvPE2VF24qQ96Pl+XUNhlg36X4eP8A5qr7HYn+/FkLH3kuVty4Nmu7qxJ6bm1lAHeYxIFF8TZPoqbcW64DXOjtQAf6LmbS+ef3UUrfE8sZIePMuOWvLHrFzKVnFDRAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEGva217prRmKORzdz4YdVttbM9aed4+bEzp7TuHSV0167neInLKR5m1x7wGt9TSSW+OldgMQ6obBau/6l7f9pOKEV6mU8q+np8PGdfWvNnut6OuGxsdI6R9XyvNXyPJc4nrLjtK9sxkcLassCuIqVrVSUgbVaOLrK3c4P4OGRu1sjPVcD1hwoVlwlbMrG9aR5x8w9LOZH+LOdxbKB1hfuLpQ0dEVxteOziqOxeLd4GOXR3w8mzq9D8vuaeltcWrjjZTb5KEA3eLuKNuI+sgbnsr85vlovk7dOWF9XsxzmTcFxWICAgICAgICAgICAgICAgICAgICAg1jmJr7E6I03NmL/7yU/d2NmDR885FWsHUOlx6AumrXc7xE5ZcR4u1TqzOarzk2ZzU5nupdjGCojijr6sUTfmtb6d52r7GvCYziPLleWOY5do51ZjKuIq1GFUTVhjVUSmaxaxIGKh9DFrCP8baXsGSxlw+yytq7jtruI8LmuHQesHcQVz26ZnOKvDZca9O8nebEOtsbJZ5BrbXU2OaBf2w2NlbuE8Q+i4+0PmnsIX5/wAnx7rvw+lr2d0djLzOggICAgICAgICAgICAgICAgICAgEgCp3IPFfOrmDLrLWlxLDIXYbGl1ri2A+qWtNJJu+Vwr9Wi+v4+rsx+Xlzy5rQmb16IipWnaqiKtRFXE1dhVRFXI2qolZYxVGJBGqjHLw1oFiqMfbHLZPTuas9S4k8N/jX8bmbhNCdkkT6dDm1C8/k6Zni7adnbXsjTuesM/grHNWDuOzv4WzxHpAcNrT2tOw9q/NZY3G8V9OXlkVLRAQEBAQEBAQEBAQEBAQEBAQEGj869TP07y1zF5C/gu7iMWdq4bxJcnw6jta0ud5F28fDuziM7xHiQAAADcF9h5nJu9IypGq4irMRVRlXYXblcRV+EqohdiG5VGLDWLWPoa0kgEEt9oDeO9aV8cxVGI3NG4ioO8LR3T7sWeecVmtLzPr/AKVcNubME7RBdVJaOxsjSfzl+f8AyGrtz5fT8fPnF3cvA7iAgICAgICAgICAgICAgICAgIOi/ezvnx6WwdiDRlzfOleOvwYXAemRe3wp+61y29HmFfRcH1oJIA2k7kYl8Odm10Zp1gKvVPo5xzx9Joe1VMoy41chniO5486uWIsrI27waUNVcRVqeW7itzLbRiV7drozWpHZTpS2yejJJ9VKDVnD+3tSG7i5jviI+Nc5u+HS6fl8yOVsbhrL2xuDBew7HMcKF7K7vouomWcvrOpjhZ6XoyOPyonLIbloiuHisbh7Eg62nr7F1w2c+lc88OOi69i7IbxyBuXW3NSSEexf4qVrh1mGWNwPmXyvyePpK9niV6aXxXuEBAQEBAQEBAQEBAQEBAQEBAQdB+9vC44TTkwHqsu52E9r4gR+ovb4XWuO55pX0XFmcLjTKQ9w2u3dy6YYuOzNtdthWFo2LtMXmuawdM2co+8hY/vaEuEJsrg7QWIl/clh/Ic4fKouuKm7JwPLOwdtZPPH3Fp+JT2K/voOWB+ZkZ2+QfEQs7flv93wvYjl5c2E5eL4zwvP3sMkQId214th7VPBls5ZO70DiLpp8S1ZxfTYOB3nbRGTZYxcugooLN9rxvkhrxQl1OOM9jh1HaFiu9iLU3LZJrG8/wAZaEB7t3Gx3sSDv6V6tWfMRnOPWN75G27n81rZ43Q427c7uL4mj4V4vyd/ZHp8TrXptfDe8QEBAQEBAQEBAQEBAQEBAQEBB5395XXmkcvhf5bsbk3GZxmQZJO1sb/DZwMkjkb4hHCXAuFQF7/E1ZS916WOGzOX0eerWEzTsj6zt7gvfI4W8RveHtAxrdi9WEeTOtjt2AAbF0cKvQsHUsF6GMdSmi7FC09C52ti3Hbt6lFqlqO2b1KbWp22g6lFqkdzjmuadizlsdda4xQs8hj8kwUD3mzuD1iQF0ZPc9vpXXTl+5fWNu5C/hbfVuXyFwS0RWcNpGaEjinldI70RNXD8jjcuJHbxs5j1+r0KvivoCAgICAgICAgICAgICAgICAgIPEvM3EPsdcZ+0cCKXs7216WyvMjT9l4X3NV5wl+Hhy9Mq13T0HHdPcR7AA8pP8AQuuE9UbL6N5sWBrQvRHkyZWDoVIZCALKxkIAorV+FqitXomLnapciYotatxxqLVJHQghTy1p3MbG+LpW/kaKvtgy5Z3wva/4AVWGXqvHqyfKjGGPCi6p95lbsytPXE0iGPzhhd5U8jPm2mM6R3ovivqiAgICAgICAgICAgICAgICAgIPNfvIaWntdVQZ5kZNnlImxySAbBcQjh4T9aPhI7ivq+Fnzj2+zyb8eLy6vxNmIZJHjdI4O9C9+MeXKtjt9gC6xxrIwHctRWStysYyUHQorYyMAUVq/CFzrYvRNXOqW42qKpMGbFLVPK4qPI4+6sXnhbdwyQl2+gkYW18lUlbKzekcXFDcWNnbs4baxja1o6mRN4W+mi5b8uMa66Zzm35fOfREBAQEBAQEBAQEBAQEBAQEBAQYzUum8VqPDXGJycXiWtwN42OY8bWyMPQ5p3K9ey4XmJyxlnFeYtYaCymj8oLO8ImtpuJ1leN2NlY0itW/Nc2o4h5l93Rumycx87bruN9WNiNF6Y89X7dyJZK3KVjJ253KKMnbrnWsjD0LnVReiUVUW41zrU7VjVizs57y4bBA2ryK7dgA6SVGWUxnNVjhcrxG64nFQ4+Dgb60r9ssnWezsC8GzZcq+jq1zGLq5uggICAgICAgICAgICAgICAgICAg6v5/Ysz6Yssi0VNhdBrz1MnbwH9MNXv/AB+fGfHu83k4848+zoqMr7MfOq7A5alk7Z25KxlLdyisZS3corWRgO5c6qL0TlzrVqNyiqTtepa2bR1uS+4uTuAEbfLtPxLyeVl0j1+Lj1rZ15HsEBAQEBAQEBAQEBAQEBAQEBAQEBBidWYNmd03kcS7Y67gcyJx+bIBxRu/NeAVevPtyl9k5Y8zh5OaJGuLJWlkrCWSMO9r2mjmnuIov0mN5nL5OU4q3C5UisjbP3JUsrbv3KaxlLd+5c61kYHKK1eieudUssepsasRHicB0byprXYWCtDa4yJjhR7x4j+9234F8zdlzk+npx7cWQXJ1EBAQEBAQEBAQEBAQEBAQEBAQEBAQec+culnYXVz8hCymPzVZ2Ebm3LaeMz87Y/ylfY8Hdzj236PB5Ovi8tKidtX0HjrIW79y1LKWz9ymsZS3fuU1rJQP3LnWrsT1FjVlj1LWw6bxhu7yNrx6g+8l+qNw8q82/Z2x304d2TsBfMfTEBAQEBAQEBAQEBAQEBAQEBAQEBAQEGB1xpO11Tp64xcxEcxpLZzkV8KdnsO7uh3YSumrZcMuYjPDunDzBd2N7jr6awvojBeWzzHPEehw6j0g7wekL9Dr2TKcx8rPC43hNA7aF0c2Ttn7llSylu/cpoyUD1FavROUVrJWUQcDNKQ2GOpc47Bs27z0LllVSOzcFjBY2YDqGeWjpSPQ3yL5O7Z3V9XTr7Z8siuTqICAgICAgICAgICAgICAgICAgICAgICDyNz81VLiOc1/HKDLj3W1oJmD2mO8KvGzyHaF7/H23CR5tuEyqLHzwXlsy7s5G3Fs/a2Rm3yEbwexfVw3Y5Pn567GTt37l1c2VtnblNYylsS4gAVPUNq51sZu1sQyF1zevbb20Y45HPIbRo6XE7GhcMs/ZcxaDrLmFHlHjF4glmKY4Cab2TNQ7gOiP4V11a+PW9V8PUcRDo2EbQWgg+Rfnq+u5ICAgICAgICAgICAgICAgICAgICAgICDRNe859E6PgkZcXbb/KtB8PF2rg+Uu6PEIq2IdZd5AV216Ms/wBEZZyPHGt8zk9UZ69z+Tp+Mv3+I5ja8LGgBrI216GMAAXuy1yTiPP381hcRlsvhLoz46d0RJ9eM+tG+n0mHYVGMsVeK7Ew/N7HcLW5rHOY8e1Nb0kae3gcQ4ecrtNljjlp5bRa81+WYaHPuXMd9B1tLX0NIT+++6P6L7Odzz10vbRluGsZ7yX5rntFvHXtrxPPmSXuJpsaRn9f6i1I8C/nEdoDVllDVkI6iRWrz2uK74YyN7eFe1lqKda741Fj1xoTXmOymBsXTyhj/CYwzH2S5oAcHfRcDvqvjb/GsyvD1avInHFbmx7HtDmODmnc4GoPmXjep9QEBAQEBAQEBAQEBAQEBAQEBBjsxqPAYWAz5fI21hFSvFcSsjr3BxFfIqxxt6RlsjrrPe8vyzxvEyzmuMvMNwtISGV/vJvCb5qrvj4md+EXbHXmZ96zUt450WnsFBag7GyXLn3MnfwR+E0ecr0YeFPreXPLc0fN625sana5mWzM8FpJ7VtG4W0VOoxw8Jd+cvXh4knSOGW/5YGLBWlqOJx8aTfUija93yrv2cOV2WqN9CHErllivGsPNbCp2Ll2ukqrNBsU3FUqiYDxrj2r5ZC0jpRd8Ii1lbcHYvRHOstaV2Lpi55Nt05ncnipC+zl4Wvp4kTvWjf3t+NXcJl1cbXYmH5kTRgGstlL0uhcXRnvafkK45+JL8mO249K2uy5rZJoHE6C9Z1OBjf6P7K82X4/H5jtj5mU+Wds+beEeQ2+tprV3S5tJWeijv0V58vx2f0srtj5uP1nDZMbqvTmToLLIQyvP7su4H/Ydwu9C8ufj54dZXow3YZdKyq4uogICAgICAgICAgIPj3sjY573BjGAuc5xoABtJJKDqbW/vI6IwDpLXE8WeyDKtItnBts1w2etOQQfzA5erX4uWXX0c8tkjpfUHPbmrqZz47O5/0mzdsEWPb4ZA/KuH1k8xC92rw8fblwz3fLT/5fvbyc3OSvHSzvNXyOc6WQnte8r24+O893ezIW+CxUNCYvFcOmQ19GwLpNWMc7syq+18cTeGNrWN6mgAehWhFLcdqi1sijcT1BXOukjGXD61XOrjHygFTYuK0sdVFipVY2+3co7VcrUEVF0xibV6Fu1dJEVlLUbl1jnkzFq6lF0jlWWt5aUXSOdZGGfcqiLFyO4qKVPnTtiXJzQ/c4V7U7WMrjNZatw1Pwd/KYW/uZD40VPqvrTyUXn2+Jrz6x2w8jPHpW9YDnhaSFsOdtDbk7Dd21Xs73Rn1h5CV83d+Ms9cLz+r26/On/qOycblMdk7Vt3j7mO6t37pI3Bw7j1HsK+XnhljeLOK92OUynMWlKhAQEBAQEBBhdX6wwWksJNmM1P4NtH6rGDbJLIfZjib85zv6TsV4YXK8RlvDybzA5tax5hXclpGXWGCDvu8bE4hpbXY65kFPEd2ez1DpX1vH8WTp615dm33a/ZYC0gAdN9/IOvYwdw+VfRw0ydfV5MttvRkwQAAAABuA3Lq5niUWWjgZu1Ta3hE+ftUWqkVpbjtUWqkU5ZqqLVyKkr6qVRXeVikZFVg+BizgTRsVSMqzEFcTV+3NFcc6yVu/cusRWQhmoqiLF2KdWmxajuKdK1PCwy57VqeEzbntRnDhL4Ug9YUP0hsKccj7i8/ndOXgvsVcuicP2gG1jwPmyM3OC8+/RjnOMpy669lxvMd78vOZuL1bAbdwFpmYW8U9mTUOA3yRE+03rG8elfn/ACfFuu++L62nfM/1bovK7iAgICAgjuLiC2t5bi4eIoIWOklkcaNaxgq5xPUAEkHjDmLrfJcx9Wy3fG6LB2ZdHjYDuZDWnGR/ElpU9W7oX2/F8bicf7eLbtVYIYbeIRQtDWDo6+0lfTkknEeK22+rkXLRwdIptbwhfLRTa3hC6ZRaqRBJMptVIrvlUWqkV3yVU2qiu96xvCMlY18BQSNWxiViqMWI1UStROVRNXYXrpEVcjkKqJq1HKQqieE4mWpTsnWs4TNnVRnDmJ+1azgMyHCi+5vcVeQ5fFzOt7q1eJGPb80jp7Qdzh1Lz7tUsvs668uK9ScvtZW2rtMWuXiAZOaxXsANfDnZ7be4+03sIX5nfq7MuH2NefdOWxrksQEBAQdXe8dqGbE8tLm1t3cNxmJ47BtDQ8D6vlp3sjLfKvT4mHOf6Oe28R5tsbVtpashG8bXnrcd6/SYY9s4fLyy5qVz1trETnqbW8InyqbWyK0kyi1UiB0qnlXCF0qm1XCF0ilvCJzljUbnLGuBKwAVokYVomYVUSnjVRizGVUTVqJyuIWmPVxNWGSLUpRJsWsc2S7U5LE7JVUTwlEipnDkJFo+PLXtLXbWuBBHYUG6+7PnpbTVeX05K/7q5hM8bT/Ft3BtR9aN+3uXwfyGHpz7V9Pxsv8At6QXynrEBAQEHR/vPhz7XS8R/Z/i7iVw7Y4Rw/rL6X42c515vKv7XSDnL7lr58Qvep5ahfIptUrySqLVSKkk21c7VSITIs5Vw4F6xvDgXrBGXLGuJKNfCVjAI1zYqYmYVsTU7CrjFiMqomrLCqiVhjlaU7XreWOYet5YNloVnJwsRyK5U2J2vVRKQOVD7xIMtyXkMfPCxa3YJYrgO7a2rnfCF8j8j0v+Hv8AG+j1ovhvcICAgIOlfeZjJsNOydDbm4b9qIH+qvp/i/539Hl8v+LoV7l9mvDFd71NrVeSRc7Vqk0tFFqpFN8u1c7V8PnGnLXEuQfCUHyqNfFjHxGgQc2rYxM1VGJ2KolOwqoyrDCqianY5WlKHLWOXHsRjh4lHKeWrMMlVcqbFlj9i6RKZrlSXMFaMvyWb4nPKwI+ZFck+S1cPjXx/wAjfS/4e/xfo9bL4j3CAgICDqj3jrIzaNsboDbaX7CexskcjPhIX0Pxt/8Ap/h5vKn7Xm6Ry+3XhVZHrnaqKsj1Fq5FKeTYuWVVIqF9So5W5By04farWFUaIx8RojQIxzatYmaqjEzFUSsMVMqZhVRKZpVJrkHJyBk2LORC6X1lNreFq3l2K8amxdjeusQsMcriamBVMbL7u1o685v3t2BVllZ3DyeoudHEPhK+H+Ry9L+r6XjTo9Ur5D1iAgICDD6u0zZ6m09eYW7eYo7po4ZmgF0b2uDmPAO+jguunbdeUyn0Tnh3Th5d1zys1bpVz5bq3N3jgfVyNsC6OnR4g9qM/W2dq+7q8rDZ06+z5+eq49WgSvV2sipK9c7VRSneuWVXIqh/rKJVpWlXGOYK0fQtYICNEAIxzatYmaqjEzFUSnYqYlYVSUoKMC5ORwc/YstbwqyS0cuVquFu1lrRdMKjKMlC9d5XOrbCukTV3CYzN6gvDY6dsJMndg0e6PZBFXpmmPqMHlquO7ycNc5tdMNNyr0ByZ5RHQdpeXd/dNvM5k+D8U+IERRMYS4Rxk+s71nVc4gV6l+e8jf/AGX4fT14drspeZ0EBAQEBB8c1rmlrgHNcKOadoIPQUHWus+QeitQmS5tIjh8g+pMtqAInO/Ki9n7NF7dXm5Y+mX7p/28+fjy9PR0bq7kBrnBl8sEbchaNqRPACdn5QA4h5Qvdhuwz6X19q8+Uyx6z/TrHJYXMWnF49pK1o3vDeJvnbVbnhlPo3HZjelYlj/Wp0rjK6rDCukSkBVDktH1aCAg+hGObVTErVrKmYqiU7VTErVsS5VQcHOWWiF8ii1XCjPMAdpouWVVIv41txNQRRPl+o0u9IXTXUZ8Ru+muXuts+8NxuOd4ZNDcSmkbe9w9X9JXnuxw/leP+eyMcbl0nLt7Svu4YyLguNVXr8k8UP4CAmK37nEUc9fO3fkbfTH/n+Hr1+Nx1dvYzFYzFWcdljbWKztIhSOCBgYweRq+dlncrzXqmMnRaUtEBAQEBAQEBAQYPN6J0xmiX31hGZz/wBxH93L9tlCfKu+rydmHSuWzRhl1jr3P+71hrsufZyxSV3RXkTXH/esAd+ivZj+Ql/nj/p5r4ln8cmhZX3eZLckuxMob/FsZTIPsniP6K9GO/Tl0vDncd2Py1i45L2wcWMv7i2k+hPEDT/hldO2fSo/us6xUk5I5s7bbJW0vUHtkjPoD1K5uipLyW10z9nFbTj8icD9cNWd0V/ZFKXlPzCj/wDjvk/u5IX/AAPTuje+KknLnXkZo7A3n5sfF+qSndG98Qu0TrFho7B3wP8A48h+ALZlGd0fW6O1b/8AiX3+Xl/squ6HdEjNHatJ/wDS33+Xk/sre6Mti5FoTWj6cOEvNu6sTh8NFvfPdPMXYeW+uX//ACJm/XMbfhct/sx92Wxbj5X6yPt2scX15mf1S5P7InuidvK7PD9tcW0XXQvefQ0Le9NzjkOWtDSbIFzvoxR7fSfiW8cs/t+GaxfJK9veEwY+8uWndJKRBH5yGfCuWW3Vj1qpNmXSN2wfu5Rtc2S+NrZjpbGw3Ev2pKNHpXny8/XP448u08XO/wArw7FwnKrRuK4H/hPxkzNokuj4gB7I6CMfZXk2edsy+vE+HfDxcMfpy25jGRsDGNDWNFGtaKADsAXjtel9QEBAQEBAQEBAQEBAQEBBHNb287eGaJkrfovaHD0rZbOjLJerHTaV0/KSTZRsJ6Y6s/VIXWeRnPq53Rhfoqv0Vif3b5Y+wOBHpCueVk53xcULtFsHsXR/OYD8BCr7r4TfF+UZ0fdD2blh72kfKt+5nsn7W+7h/KOQ6Jov0vkW/c4+zPtcvc/lLI/xovO75E+5x9j7XI/lHIdM0Xnd8ifc4+x9rl7vo0feH2riMdwcfkT7mezftb7uY0SXe3d0+qz5Ss+6+G/a/Lk3QeNP7aeZ/YC1vxFT93l9IqeLj9asw6H01HtdaeMeuVzneitFN8rZfqueNhPoydpi8bZ/4W1hgp0xsa0+cBcstmV611xwk6RaUKEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEH/9k="}`
      }
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addUser(user: IUser): void {
      this.users.push(user);
      this.currentUser = user;
    },
    updateUser(user: IUser): void {
      const index = this.users.findIndex((u) => u.userId == user.userId);
      this.users.splice(index, 1, user);
      this.currentUser = user;
    },
    deleteUser(userId: number): void {
      const index = this.users.findIndex((u) => u.userId == userId);
      this.users.splice(index, 1);
    },
    clearStore() {
      this.users = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IUser[] {
      UserApi.getAll()
        .then((response) => {
          this.users = response.data;
          useDashboardStore().startStore(this.users);
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiGetAll() | ", error);
        })
      return this.users;
    },
    apiGetById(userId: number): IUser {
      UserApi.getById(userId)
        .then((response) => {
          this.currentUser = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiGetById(userId) | ", error);
        })
      return this.currentUser;
    },
    apiCreate(user: IUser): boolean {
      UserApi.create(user)
        .then((response) => {
          if (response.status == 200) {
            this.addUser(response.data);
            console.log('%c Success! User created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserStore - apiCreate(user)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiCreate(user) | ", error);
          return false;
        })
      return false;
    },
    apiUpdate(user: IUser): boolean {
      UserApi.update(user)
        .then((response) => {
          if (response.data == true) {
            this.updateUser(user);
            console.log('%c Success! User updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserStore - apiUpdate(user)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiUpdate(user) | ", error);
          return false;
        })
      return true;
    },
    apiDelete(userId: number): boolean {
      UserApi.delete(userId)
        .then((response) => {
          if (response.data == true) {
            this.deleteUser(userId);
            console.log('%c Success! User deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserStore - apiDelete(userId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiDelete(userId) | ", error);
          return false;
        })
      return true;
    },
    /**
     * Testing 
     * @returns Test
     */
    apiGetAllDummyUsers(): IUser[] {
      UserApi.getAllDummyUsers()
        .then((response) => {
          this.users = response.data as IUser[];
          return this.users;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiGetRelatedUsers() | ", error);
        })
      return this.users;
    },
    /**
     * Testing 
     * @returns Test
     */
    apiGetFilteredDummyUsers(userIds: number[]): IUser[] {
      UserApi.getFilteredDummyUsers(userIds)
        .then((response) => {
          this.users = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - apiGetRelatedUsers() | ", error);
        })
      return this.users;
    },
    //Other methods
    //Create string base64 from image
    async createBase64Image(imageFile: File): Promise<string> {
      return new Promise((resolve) => {
        const reader = new FileReader();
        reader.onload = () => {
          let imageString = reader.result as string;
          return resolve(imageString.split(',')[1]);
        }
        return reader.readAsDataURL(imageFile);
      })
    },
    updateUserP(user: IUser){
      UserApi.updateUser(user)
        .then((response) => {
          if (response.status == 200) {
            this.updateUser(response.data);
            useAuthStore().updatePhoto(response.data);
            console.log('%c Success! User updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserStore - updateUserP(user)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserStore - updateUserP(user) | ", error);
          return false;
        })
      return true;
    }
  }
})



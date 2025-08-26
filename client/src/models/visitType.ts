export const VisitType = {
  FirstVisit: 1,
  FollowUp: 2
} as const;

export type VisitType = typeof VisitType[keyof typeof VisitType];